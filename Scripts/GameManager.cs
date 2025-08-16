using System.Collections.Generic;
using Godot;

namespace MicroTurnBasedRPG.Scripts;

public partial class GameManager : Node2D {
    [Export]
    private Character playerCharacter = null!;

    [Export]
    private Character aiCharacter = null!;

    [Export]
    private EndScreen endScreen = null!;
    
    private Character? currentCharacter;
    private bool gameOver;

    private CombatActionsUi playerUi = null!;
    
    public override void _Ready() {
        base._Ready();
        playerCharacter.OnTakeDamage += OnPlayerTakeDamage;
        aiCharacter.OnTakeDamage += OnAiTakeDamage;
        playerUi = GetNode<CombatActionsUi>("CanvasLayer/CombatActionsUi");
        endScreen.Visible = false;
        NextTurn();
    }

    private void OnPlayerTakeDamage(int health) {
        if (health <= 0) {
            EndGame(aiCharacter);
        }
    }

    private void OnAiTakeDamage(int health) {
        if (health <= 0) {
            EndGame(playerCharacter);
        }
    }

    public void EndGame(Character winner) {
        gameOver = true;
        endScreen.Visible = true;
        if (winner == playerCharacter) {
            endScreen.SetHeaderText("You won!");
        }
        else {
            endScreen.SetHeaderText("You lost!");
        }
    }

    
    public async void NextTurn() {
        if (gameOver) return;

        currentCharacter?.EndTurn();

        if (currentCharacter == aiCharacter || currentCharacter is null) {
            currentCharacter = playerCharacter;
        }
        else {
            currentCharacter = aiCharacter;
        }
        
        currentCharacter?.BeginTurn();

        if (currentCharacter is {IsPlayer: true}) {
            playerUi.Visible = true;
            playerUi.SetCombatActions(playerCharacter.CombatActions);
        }
        else {
            playerUi.Visible = false;
            var waitTime = GD.RandRange(0.5, 1.5);
            await ToSignal(GetTree().CreateTimer(waitTime), SceneTreeTimer.SignalName.Timeout);
            var actionToCast = AiDecideCombatAction();
            aiCharacter.CastCombatAction(actionToCast, playerCharacter);
            await ToSignal(GetTree().CreateTimer(0.5f), SceneTreeTimer.SignalName.Timeout);
            NextTurn();
        }
    }

    public async void PlayerCastCombatAction(CombatAction action) {
        if (currentCharacter != playerCharacter) return;

        playerUi.Visible = false;
        playerCharacter.CastCombatAction(action, aiCharacter);
        await ToSignal(GetTree().CreateTimer(0.5f), SceneTreeTimer.SignalName.Timeout);
        NextTurn();
    }
    
    private CombatAction? AiDecideCombatAction() {
        var ai = aiCharacter;
        if (ai != currentCharacter) {
            return null;
        }

        var player = playerCharacter;
        var actions = ai.CombatActions;
        var weights = new List<int>();
        var totalWeight = 0;
        
        var aiHealthPercentage = (double)ai.CurrentHealth / ai.MaxHealth;
        var playerHealthPercentage = (double)player.CurrentHealth / player.MaxHealth;

        foreach (var action in actions) {
            var weight = action.BaseWeight;
            if (player.CurrentHealth <= action.MeleeDamage) {
                weight *= 3;
            }
            if (action.HealAmount > 0) {
                weight *= 1 + (int)((1 - aiHealthPercentage) * 2);
            }
            
            weights.Add(weight);
            totalWeight += weight;
        }

        var cumulativeWeight = 0;
        var randomWeight = GD.RandRange(0, totalWeight);

        for (var i = 0; i < actions.Count; i++) {
            cumulativeWeight += weights[i];
            if (randomWeight < cumulativeWeight) {
                return actions[i];
            }
        }
        
        return null;
    }
}
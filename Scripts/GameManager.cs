using Godot;

namespace MicroTurnBasedRPG.Scripts;

public partial class GameManager : Node2D {
    [Export]
    private Character? playerCharacter;

    [Export]
    private Character? aiCharacter;

    private Character? currentCharacter;
    private bool gameOver;

    private CombatActionsUi playerUi = null!;
    
    public override void _Ready() {
        base._Ready();
        playerUi = GetNode<CombatActionsUi>("CanvasLayer/CombatActionsUi");
        NextTurn();
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
            aiCharacter?.CastCombatAction(actionToCast, playerCharacter);
            await ToSignal(GetTree().CreateTimer(0.5f), SceneTreeTimer.SignalName.Timeout);
            NextTurn();
        }
    }

    public async void PlayerCastCombatAction(CombatAction action) {
        if (currentCharacter != playerCharacter) return;

        playerUi.Visible = false;
        playerCharacter?.CastCombatAction(action, aiCharacter);
        await ToSignal(GetTree().CreateTimer(0.5f), SceneTreeTimer.SignalName.Timeout);
        NextTurn();
    }
    
    private CombatAction? AiDecideCombatAction() {
        return null;
    }
}
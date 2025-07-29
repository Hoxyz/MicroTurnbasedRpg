#nullable enable
using Godot;

namespace MicroTurnBasedRPG.Scripts;

public partial class GameManager : Node2D {
    [Export]
    private Character? playerCharacter;

    [Export]
    private Character? aiCharacter;

    private Character? currentCharacter;
    private bool gameOver;

    public override void _Ready() {
        base._Ready();
        NextTurn();
    }

    private async void NextTurn() {
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
            
        }
        else {
            var waitTime = GD.RandRange(0.5, 1.5);
            await ToSignal(GetTree().CreateTimer(waitTime), SceneTreeTimer.SignalName.Timeout);
            await ToSignal(GetTree().CreateTimer(0.5f), SceneTreeTimer.SignalName.Timeout);
            NextTurn();
        }
    }

    private async void PlayerCastCombatAction() {
        if (currentCharacter != playerCharacter) {
                        
        }

        playerCharacter?.CastCombatAction(0, aiCharacter);
        await ToSignal(GetTree().CreateTimer(0.5f), SceneTreeTimer.SignalName.Timeout);
        NextTurn();
    }
    
    private void AiDecideCombatAction() {
        
    }
}
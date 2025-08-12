using Godot;
using Godot.Collections;

namespace MicroTurnBasedRPG.Scripts;

public partial class CombatActionsUi : Panel
{
    private VBoxContainer buttonContainer = null!;
    private RichTextLabel descriptionLabel = null!;
    private GameManager gameManager = null!;

    private Array<CombatActionButton> combatActionButtons = null!;
    
    public override void _Ready() {
        base._Ready();
        buttonContainer = GetNode<VBoxContainer>("ButtonContainer");
        descriptionLabel = GetNode<RichTextLabel>("DescriptionLabel");
        gameManager = GetNode<GameManager>("/root/Main");
    }
}
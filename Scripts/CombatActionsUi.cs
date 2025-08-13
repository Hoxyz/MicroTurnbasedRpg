using System;
using System.Collections.Generic;
using Godot;
using Godot.Collections;

namespace MicroTurnBasedRPG.Scripts;

public partial class CombatActionsUi : Panel
{
    private VBoxContainer buttonContainer = null!;
    private RichTextLabel descriptionLabel = null!;
    private GameManager gameManager = null!;

    private Array<CombatActionButton> combatActionButtons = [];
    
    public override void _Ready() {
        base._Ready();
        buttonContainer = GetNode<VBoxContainer>("ButtonContainer");
        descriptionLabel = GetNode<RichTextLabel>("DescriptionLabel");
        gameManager = GetNode<GameManager>("/root/Main");
        
        foreach (var child in buttonContainer.GetChildren()) {
            if (child is not CombatActionButton caButton) continue;

            combatActionButtons.Add(caButton);
            caButton.Pressed += () => ButtonPressed(caButton);
            caButton.MouseEntered += () => ButtonEntered(caButton);
            caButton.MouseExited += () => ButtonExited(caButton);
        }
    }

    private void SetCombatActions(List<CombatAction> combatActions) {
        for (int i = 0; i < combatActionButtons.Count; i++) {
            if (i >= combatActions.Count) {
                combatActionButtons[i].Visible = false;
                continue;
            }
            
            combatActionButtons[i].Visible = true;
            combatActionButtons[i].SetCombatAction(combatActions[i]);
        }
    }
    
    private void ButtonPressed(CombatActionButton button) {
        gameManager.PlayerCastCombatAction(button.CombatAction);
    }

    private void ButtonEntered(CombatActionButton button) {
        var combatAction = button.CombatAction;
        descriptionLabel.Text = "[b]" + combatAction.DisplayName + "[/b]\n" + combatAction.Description;
    }

    private void ButtonExited(CombatActionButton button) {
        descriptionLabel.Text = "";
    }

    public void OnPassTurnButtonPressed() {
        gameManager.NextTurn();
    }

}
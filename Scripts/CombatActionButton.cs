using Godot;
using MicroTurnBasedRPG.Scripts;

public partial class CombatActionButton : Button
{
    private CombatAction combatAction;

    public void SetCombatAction(CombatAction combatAction) {
        this.combatAction = combatAction;
        Text = combatAction.DisplayName;
    }
}

using Godot;
using MicroTurnBasedRPG.Scripts;

public partial class CombatActionButton : Button
{
    public CombatAction CombatAction;

    public void SetCombatAction(CombatAction combatAction) {
        CombatAction = combatAction;
        Text = combatAction.DisplayName;
    }
}

using Godot;

namespace MicroTurnBasedRPG.Scripts;

[GlobalClass]
public partial class CombatAction : Resource {
    [Export]
    public string DisplayName = null!;

    [Export]
    public string Description = null!;

    [Export]
    public int MeleeDamage;

    [Export]
    public int HealAmount;
    
    [Export]
    public int BaseWeight = 100;

}
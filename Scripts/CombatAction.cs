using Godot;

namespace MicroTurnBasedRPG.Scripts;

[GlobalClass]
public partial class CombatAction : Resource {
    [Export]
    public string DisplayName = null!;

    [Export]
    public string Description = null!;

    [Export]
    private int meleeDamage;

    [Export]
    private int healAmount;
    
    [Export]
    private int baseWeight = 100;

}
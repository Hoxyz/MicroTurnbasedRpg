using Godot;

namespace MicroTurnBasedRPG.Scripts;

[GlobalClass]
public partial class CombatAction : Resource {
    [Export]
    private string displayName;

    [Export]
    private string description;

    [Export]
    private int meleeDamage;

    [Export]
    private int healAmount;
    
    [Export]
    private int baseWeight = 100;

}
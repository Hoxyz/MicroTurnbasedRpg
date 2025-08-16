using Godot;

namespace MicroTurnBasedRPG.Scripts;

public partial class CharacterHealthBar : ProgressBar
{
    [Export]
    private Label healthBarLabel = null!;

    [Export]
    private Character character = null!;
    
    public override void _Ready() {
        base._Ready();
        MaxValue = character.MaxHealth;
        UpdateValue(character.CurrentHealth);
        
        character.OnTakeDamage += UpdateValue;
        character.OnHeal += UpdateValue;
    }

    private void UpdateValue(int health) {
        Value = health;
        healthBarLabel.Text = health + " / " + (int)MaxValue;
    }
}
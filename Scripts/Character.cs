using Godot;
using Godot.Collections;

namespace MicroTurnBasedRPG.Scripts;

public partial class Character : Node2D {
    [Signal]
    public delegate void OnTakeDamageEventHandler(int health);
    
    [Signal]
    public delegate void OnHealEventHandler(int health);

    [Export]
    public bool IsPlayer;
    
    [Export]
    public int CurrentHealth;
    
    [Export]
    public int MaxHealth;

    [Export]
    public Array<CombatAction> CombatActions = [];

    [Export]
    private bool facingLeft;
    
    [Export]
    private Sprite2D sprite = null!;

    [Export]
    private Texture2D displayTexture = null!;
    
    private float targetScale = 1f;
    
    private AudioStreamPlayer2D? audioStreamPlayer;
    private AudioStream takeDamageSfx = GD.Load<AudioStream>("res://Audio/take_damage.wav");
    private AudioStream healSfx = GD.Load<AudioStream>("res://Audio/heal.wav");
    
    public override void _Ready() {
        base._Ready();
        audioStreamPlayer = GetNode<AudioStreamPlayer2D>("AudioStreamPlayer2D");
        sprite.FlipH = facingLeft;
        sprite.Texture = displayTexture;
    }

    public void BeginTurn() {
        targetScale = 1.1f;

        GD.Print(IsPlayer ? "Player turn has begun!" : "AI turn has begun!");
    }

    public void EndTurn() {
        targetScale = 0.9f;
    }

    public override void _Process(double delta) {
        base._Process(delta);
    }

    private void TakeDamage(int damage) {
        PlayAudio(takeDamageSfx);
        CurrentHealth -= damage;
        EmitSignalOnTakeDamage(CurrentHealth);
    }

    private void Heal(int healAmount) {
        PlayAudio(healSfx);
        CurrentHealth += healAmount;
        CurrentHealth = int.Clamp(CurrentHealth,0, MaxHealth);
        EmitSignalOnHeal(CurrentHealth);
    }

    private void PlayAudio(AudioStream stream) {
        if (audioStreamPlayer == null) return;
        
        audioStreamPlayer.Stream = stream;
        audioStreamPlayer.Play();
    }

    public void CastCombatAction(CombatAction? action, Character opponent) {
        if (action == null) return;
        
        if (action.MeleeDamage > 0) {
            opponent.TakeDamage(action.MeleeDamage);
        }

        if (action.HealAmount > 0) {
            Heal(action.HealAmount);
        }
    }
}
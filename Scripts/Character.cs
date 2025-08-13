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
    private int currentHealth;
    
    [Export]
    private int maxHealth;

    [Export]
    public Array<CombatAction> CombatActions = [];
    
    private float targetScale = 1f;
    
    private AudioStreamPlayer2D audioStreamPlayer;
    private AudioStream takeDamageSfx = GD.Load<AudioStream>("res://Audio/take_damage.wav");
    private AudioStream healSfx = GD.Load<AudioStream>("res://Audio/heal.wav");
    
    public override void _Ready() {
        base._Ready();
        audioStreamPlayer = GetNode<AudioStreamPlayer2D>("AudioStreamPlayer2D");
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
    }

    private void Heal(int heal) {
        PlayAudio(healSfx);
    }

    private void PlayAudio(AudioStream stream) {
        audioStreamPlayer.Stream = stream;
        audioStreamPlayer.Play();
    }

    public void CastCombatAction(CombatAction action, Character opponent) {
        
    }
}
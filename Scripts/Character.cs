using System.Xml;
using Godot;

namespace MicroTurnBasedRPG.Scripts;

public partial class Character : Node2D {
    [Signal]
    public delegate void OnTakeDamageEventHandler(int health);
    
    [Signal]
    public delegate void OnHealEventHandler(int health);

    [Export]
    private bool isPlayer;
    
    [Export]
    private int currentHealth;
    
    [Export]
    private int maxHealth;

    private float targetScale = 1f;
    
    private AudioStreamPlayer2D audioStreamPlayer;
    private AudioStream takeDamageSfx = GD.Load<AudioStream>("res://Audio/take_damage.wav");
    private AudioStream healSfx = GD.Load<AudioStream>("res://Audio/heal.wav");
    
    public override void _Ready() {
        base._Ready();
        audioStreamPlayer = GetNode<AudioStreamPlayer2D>("AudioStreamPlayer2D");
    }

    private void BeginTurn() {
        targetScale = 1.1f;
    }

    private void EndTurn() {
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
}
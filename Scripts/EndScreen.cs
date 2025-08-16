using Godot;

namespace MicroTurnBasedRPG.Scripts;

public partial class EndScreen : Panel {
    [Export]
    private Label headerLabel = null!;
    
    public void SetHeaderText(string text) => headerLabel.Text = text;

    public void OnPlayAgainButtonClicked() {
        GetTree().ReloadCurrentScene();
    }
}
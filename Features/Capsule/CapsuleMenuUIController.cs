using Godot;
using System;
using Ldjam54.Features.Capsule;

public partial class CapsuleMenuUIController : Control
{
    [Export] public Button CloseButton;

    [Export] public Button OccupyButton;

    [Export] public Button InfoButton;

    public CapsuleController Capsule;
    
    public override void _Ready()
    {
        CloseButton.Pressed += OnCloseButtonPressed;
        OccupyButton.Pressed += OccupyButtonOnPressed;
        
        GameManager.Instance.OnCapsuleSelected += Initialize;
        
        Hide();
    }

    private void OccupyButtonOnPressed()
    {
        GameManager.Instance.OccupyCapsule(Capsule);
        
        HandleHide();
    }

    private void OnCloseButtonPressed()
    {
        HandleHide();
    }

    public void Initialize(CapsuleController capsuleController)
    {
        Capsule = capsuleController;
        
        GlobalPosition = GetGlobalMousePosition();
        
        Show();
    }

    private void HandleHide()
    {
        Capsule = null;
        Hide();
    }
}

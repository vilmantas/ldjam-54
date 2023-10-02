using Godot;
using System;

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
        GameManager.Instance.OnCapsuleDeselected += OnCapsuleDeselected;
        
        Hide();
    }

    private void OnCapsuleDeselected()
    {
        HandleHide();
    }

    private void OccupyButtonOnPressed()
    {
        GameManager.Instance.OccupyCapsule();
    }

    private void OnCloseButtonPressed()
    {
        GameManager.Instance.DeselectCapsule();
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

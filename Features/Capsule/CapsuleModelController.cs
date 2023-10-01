using Godot;
using System;

public partial class CapsuleModelController : Node3D
{
	public Area3D Hitbox;

	public Node3D Outline;

	public AnimationPlayer AnimationPlayer;
    
	public Action<MouseButton> OnHitboxClicked;
    
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Hitbox = GetNode<Area3D>("hitbox");
		
		AnimationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		
		Hitbox.InputEvent += HitboxOnInputEvent;
		
		Hitbox.MouseEntered += OnMouseEntered;
		
		Hitbox.MouseExited += OnMouseExited;

		Outline = GetNode<Node3D>("Outline");
        
		Outline.Hide();
	}

	private void OnMouseExited()
	{
		GameManager.Instance.HideTooltip();
	}

	private void OnMouseEntered()
	{
		GameManager.Instance.ShowTooltipText("Capsule Actions");
	}

	private void HitboxOnInputEvent(Node camera, InputEvent @event, Vector3 position, Vector3 normal, long shapeidx)
	{
		if (@event is InputEventMouseButton mouseEvent)
		{
			if ((mouseEvent.ButtonIndex == MouseButton.Left || mouseEvent.ButtonIndex == MouseButton.Right) && mouseEvent.Pressed)
			{
				OnHitboxClicked?.Invoke(mouseEvent.ButtonIndex);
			}
		}
	}
}

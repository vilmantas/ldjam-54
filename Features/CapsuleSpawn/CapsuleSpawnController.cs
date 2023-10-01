using Godot;
using System;

public partial class CapsuleSpawnController : Node3D
{
	public Area3D SpawnArea;
    
	public override void _Ready()
	{
		SpawnArea = GetNode<Area3D>("Area3D");

		SpawnArea.MouseEntered += OnMouseEntered;

		SpawnArea.MouseExited += OnMouseExited;
		
		SpawnArea.InputEvent += OnInputEvent;
	}

	private void OnMouseExited()
	{
		GameManager.Instance.HideTooltip();
	}

	private void OnMouseEntered()
	{
		GameManager.Instance.ShowTooltipText("Build a capsule");
	}
	
	private void OnInputEvent(Node camera, InputEvent @event, Vector3 position, Vector3 normal, long shapeidx)
	{
		if (@event is InputEventMouseButton mouseEvent)
		{
			if (mouseEvent.ButtonIndex == MouseButton.Left && mouseEvent.Pressed)
			{
				GameManager.Instance.SelectSpawn(this);
			}
		}
	}
}

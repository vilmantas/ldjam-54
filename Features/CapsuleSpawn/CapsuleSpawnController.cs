using Godot;
using System;

public partial class CapsuleSpawnController : Node3D
{
	public Godot.Area3D SpawnArea;
    
	public override void _Ready()
	{
		SpawnArea = GetNode<Godot.Area3D>("Area3D");
		
		SpawnArea.InputEvent += OnInputEvent;
	}

	private void OnInputEvent(Node camera, InputEvent @event, Vector3 position, Vector3 normal, long shapeidx)
	{
		GD.Print("HERE");
        
		GD.Print(@event.AsText());
		
		if (@event is InputEventMouseButton mouseEvent)
		{
			if (mouseEvent.ButtonIndex == MouseButton.Left)
			{
				GD.Print("CLICK");
				OnSpawnClicked(GlobalPosition);
			}
		}
		
		GD.Print("Exiting");
	}

	public void OnSpawnClicked(Vector3 position)
	{
		GameManager.Instance.BuildCapsule(position);
	}
}

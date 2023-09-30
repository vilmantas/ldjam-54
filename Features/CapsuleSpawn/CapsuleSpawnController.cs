using Godot;
using System;

public partial class CapsuleSpawnController : Node3D
{
	public Area3D SpawnArea;
    
	public override void _Ready()
	{
		SpawnArea = GetNode<Area3D>("Area3D");
		
		SpawnArea.InputEvent += OnInputEvent;
		
		GameManager.Instance.OnBuildModeChanged += OnBuildModeChanged;
		
		HandleBuildModeState(GameManager.Instance.BuildModeActive);
	}

	private void OnBuildModeChanged(bool obj)
	{
		HandleBuildModeState(obj);
	}

	private void HandleBuildModeState(bool state)
	{
		if (state)
		{
			this.Show();
		}
		else
		{
			this.Hide();
		}
	}

	private void OnInputEvent(Node camera, InputEvent @event, Vector3 position, Vector3 normal, long shapeidx)
	{
		if (@event is InputEventMouseButton mouseEvent)
		{
			if (mouseEvent.ButtonIndex == MouseButton.Left && mouseEvent.Pressed)
			{
				GD.Print("CLICK");
				OnSpawnClicked(GlobalPosition);
			}
		}
	}

	public void OnSpawnClicked(Vector3 position)
	{
		if (GameManager.Instance.BuildCapsule(position, GetParent()))
		{
			SpawnArea.InputEvent -= OnInputEvent;
			GameManager.Instance.OnBuildModeChanged -= OnBuildModeChanged;
			this.QueueFree();
		}
	}
}

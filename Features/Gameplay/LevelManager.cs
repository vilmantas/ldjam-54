using Godot;
using System;

public partial class LevelManager : Node3D
{
	[Export] public Node3D CapsuleRoomWaypoint;
	
	public override void _Ready()
	{
		GameManager.Instance.LevelManager = this;
	}
}

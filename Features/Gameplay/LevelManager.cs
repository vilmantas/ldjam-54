using Godot;
using System;
using System.Linq;

public partial class LevelManager : Node3D
{
	[Export] public Node3D CapsuleRoomWaypoint;
	
	[Export] public CustomerSpawnPoint[] CustomerSpawnPoints;
	
	public override void _Ready()
	{
		GameManager.Instance.LevelManager = this;
		
		CustomerSpawnPoints = GetTree().GetNodesInGroup("CustomerSpawnPoint").Cast<CustomerSpawnPoint>().ToArray();

		var capsuleRoomWaypoint = GetTree().GetNodesInGroup("CapsuleRoomWaypoint");

		if (capsuleRoomWaypoint.Any())
		{
			CapsuleRoomWaypoint = capsuleRoomWaypoint.Cast<Node3D>()?.First();
		}
	}
}

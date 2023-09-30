using Godot;
using System;

public partial class CapsuleNodeController : Node
{
	public Node3D SpawnPoint;
    
	public override void _Ready()
	{
		SpawnPoint = GetNode<Node3D>("spawn");
	}

	public void BuildCapsule(CapsuleController prefab)
	{
		
	}
}

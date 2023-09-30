using Godot;
using System;
using Ldjam54.Features.Capsule;

public partial class CapsuleController : Node3D
{
	public CapsuleModel Data;
	
	public void Initialize(CapsuleConfiguration configuration, Node parent)
	{
		Data = new CapsuleModel() {Parent = parent};
		
		if (parent is CapsuleController)
		{
			GetNode<Node3D>("spawn").QueueFree();
		}
		
		var model = (Node3D) configuration.CapsulePrefab.Instantiate();
		AddChild(model);
	}
}

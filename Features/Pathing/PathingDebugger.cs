using Godot;
using System;
using System.Linq;

public partial class PathingDebugger : Node3D
{
	public PackedScene Pointer;

	[Export] public Camera3D Camera;

	[Export] public CustomerV2Controller Customer;

	public override void _Ready()
	{
		Pointer = ResourceLoader.Load<PackedScene>("res://Prefabs/pathing_debug_pointer.tscn");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (!Input.IsActionJustPressed("left_click")) return;
		
		var mousePos = GetViewport().GetMousePosition();

		var query = new PhysicsRayQueryParameters3D();

		var from = Camera.ProjectRayOrigin(mousePos);

		var to = from + Camera.ProjectRayNormal(mousePos) * 1000;

		var space = GetWorld3D().DirectSpaceState;
			
		query.From = from;
		query.To = to;
		query.CollideWithAreas = true;
			
		var result = space.IntersectRay(query);
			
		var z = (Vector3)result["position"];
		
		Node3D pointer = Pointer.Instantiate<Node3D>();
		
		GetTree().Root.AddChild(pointer);

		pointer.GlobalPosition = z;
		
		Customer.NavigationAgent.TargetPosition = z;
	}
}

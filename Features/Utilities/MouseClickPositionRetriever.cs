using Godot;
using System;

public partial class MouseClickPositionRetriever : Node3D
{
	public Action<Vector3> OnPositionClicked;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (!Input.IsActionJustPressed("left_click")) return;

		var Camera = GetViewport().GetCamera3D();
        
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
		
		OnPositionClicked?.Invoke(z);
	}
}

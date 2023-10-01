using Godot;
using System;

public partial class RandomHide : Node3D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GD.Randomize();

		if (GD.RandRange(0, 1) > 0.5f)
		{
			Hide();
		}
		else
		{
			RotateY(Mathf.RadToDeg(GD.RandRange(0, 180)));
		}
	}
}

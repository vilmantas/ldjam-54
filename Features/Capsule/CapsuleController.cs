using Godot;
using System;

public partial class CapsuleController : Node
{
	public Node3D LowerCapsule;
	
	public Node3D UpperCapsule;

	[Export] public int StartingPods = 0;

	public int CurrentPods = 0;

	public override void _Ready()
	{
		LowerCapsule = GetNode<Node3D>("capsule_lower");
		
		UpperCapsule = GetNode<Node3D>("capsule_upper");

		LowerCapsule.Hide();
		UpperCapsule.Hide();

		if (StartingPods == 1)
		{
			LowerCapsule.Show();
		}
		
		if (StartingPods == 2)
		{
			LowerCapsule.Show();
			UpperCapsule.Show();
		}
		
		CurrentPods = StartingPods;
	}

	public void BuildPod()
	{
		if (CurrentPods == 0)
		{
			LowerCapsule.Show();
			CurrentPods++;
		}
		else if (CurrentPods == 1)
		{
			UpperCapsule.Show();
			CurrentPods++;
		}
	}
}

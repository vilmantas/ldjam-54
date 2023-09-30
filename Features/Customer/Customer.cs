using Godot;
using System;



public partial class Customer : CharacterBody3D
{
	private CapsuleController capsule;
	private Node3D currentTarget;
	private double maxMovementTimer = 5;
	private double distanceToTargetReached = 1;
	private double movementTimer = 0;
	private bool isMoving = false;
	private bool targetReached = false;
	

	
	public override void _Ready()
	{
		
	}

	public override void _Process(double delta)
	{
		getTargetAndNavigate();
		if (isMoving)
		{
			movementTimer += delta;

			if (movementTimer >= maxMovementTimer)
			{
				teleportToTarget();
				movementTimer = 0;
				isMoving = false;
			}
		}

		if (targetReached == false && currentTarget != null)
		{
			if (Position.DistanceTo(currentTarget.Position) < distanceToTargetReached)
			{
				targetReached = true;
			}
			
		}
		
		
	}

	private void getTargetAndNavigate()
	{
		if (currentTarget == null)
		{
			currentTarget = getNextTarget();
			if (currentTarget != null)
			{
				navigateToTarget();
			}
		}
	}

	private void navigateToTarget()
	{
		targetReached = false;
		isMoving = true;
	}
	
	private void teleportToTarget()
	{
		Position = currentTarget.Position;
	}

	private Node3D getNextTarget()
	{
		if (capsule == null)
		{
			return GetTree().GetFirstNodeInGroup("Reception") as Reception;
		}

		return null;
	}

	private void onTargetReached()
	{
		
	}

	public override void _PhysicsProcess(double delta)
	{
		
		base._PhysicsProcess(delta);
	}
}

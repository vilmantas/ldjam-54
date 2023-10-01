using Godot;
using System;

public partial class CustomerV2Controller : CharacterBody3D
{
	public enum Statez
	{
		None, Idle, Moving
	}

	public Statez State = Statez.None;
    
	public NavigationAgent3D NavigationAgent;
    
	public CharacterAnimationController Animator;
	
	public override void _Ready()
	{
		Animator = GetNode<CharacterAnimationController>("model");
		NavigationAgent = GetNode<NavigationAgent3D>("navigator");
		
		ChangeState(Statez.Idle);
	}

	public override void _Process(double delta)
	{
		if (NavigationAgent.IsNavigationFinished())
		{
			ChangeState(Statez.Idle);
			return;
		}
		
		var target = NavigationAgent.GetNextPathPosition();
		
		var direction = GlobalPosition.DirectionTo(target);

		var angle = new Vector2(direction.Z, direction.X).Angle();

		var newRot = Rotation;
		
		newRot.Y = (float)Mathf.LerpAngle(newRot.Y, angle - Mathf.Pi, delta * 5f);

		Rotation = newRot;
			
		Velocity = Velocity.Lerp(direction * 3f, 5f * (float)delta);
        
		MoveAndSlide();
		
		ChangeState(Statez.Moving);
	}

	private void ChangeState(Statez state)
	{
		if (State == state) return;

		State = state;
		
		switch (State)
		{
			case Statez.Idle:
				Animator.PlayAnimation("Idle");
				break;
			case Statez.Moving:
				Animator.PlayAnimation("Walking");
				break;
			case Statez.None:
				break;
			default:
				throw new ArgumentOutOfRangeException();
		}
	}
}

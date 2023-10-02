using Godot;
using System;
using System.Linq;
using Ldjam54.Features.GameplayConfigurations;

public partial class CustomerV2Controller : CharacterBody3D
{
	public enum CustomerState
	{
		None, Idle, Moving
	}

	public CustomerState State = CustomerState.None;
    
	public NavigationAgent3D NavigationAgent;
	public CharacterAnimationController Animator;
	
	public Reception Reception;
	public Vector3 SpawnLocation;
	public Area3D Clickbox;

	public CustomerData Data;

	public bool CheckedIn = false;
	public bool AssignedCapsule = false;
	public float CurrentPatience;
	
	public override void _Ready()
	{
		SpawnLocation = Position;
		Clickbox = GetNode<Area3D>("clickbox");
		Animator = GetNode<CharacterAnimationController>("model");
		NavigationAgent = GetNode<NavigationAgent3D>("navigator");
		NavigationAgent.NavigationFinished += OnNavigationFinished;
		ChangeState(CustomerState.Idle);

		Clickbox.MouseEntered += ClickboxOnMouseEntered;
		Clickbox.MouseExited += ClickboxOnMouseExited;
		Clickbox.InputEvent += ClickboxOnInputEvent;
		
		GD.Randomize();

		var data = new CustomerData()
		{
			Name = "Test " + GD.Randi(), 
			StayDuration = 5f + GD.RandRange(0, 10),
			Patience = GD.RandRange(10, 20),
			PreferredCapsule = CapsuleConfigurations.Capsules[GD.RandRange(0, CapsuleConfigurations.Capsules.Length - 1)] 
		};
		CurrentPatience = data.Patience;
		Initialize(data);
	}

	public void Initialize(CustomerData data)
	{
		Data = data;
	}
	
	private void ClickboxOnInputEvent(Node camera, InputEvent @event, Vector3 position, Vector3 normal, long shapeidx)
	{
		if (@event is not InputEventMouseButton mouseEvent) return;
		
		if (mouseEvent.ButtonIndex == MouseButton.Left && mouseEvent.Pressed)
		{
			GameManager.Instance.SelectCustomer(this);
		}
	}

	private void ClickboxOnMouseExited()
	{
		GameManager.Instance.HideTooltip();
	}

	private void ClickboxOnMouseEntered()
	{
		GameManager.Instance.ShowTooltipText("Customer");
	}

	public override void _Process(double delta)
	{
		Navigate(delta);
		HandlePatience(delta);
	}

	private void HandlePatience(double delta)
	{
		if (CheckedIn && !AssignedCapsule && CurrentPatience > 0)
		{
			CurrentPatience -= (float)delta;
		}

		if (CurrentPatience < 0)
		{
			if (Reception != null)
			{
				Reception.ExitQueue(this);
			}
			
			NavigationAgent.TargetPosition = SpawnLocation;
		}
		
		
	}

	private void OnNavigationFinished()
	{
		if (CurrentPatience < 0 && NavigationAgent.TargetPosition == SpawnLocation)
		{
			QueueFree();
		}
	}

	private void Navigate(double delta)
	{
		if (NavigationAgent.IsNavigationFinished())
		{
			ChangeState(CustomerState.Idle);
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
		
		ChangeState(CustomerState.Moving);
	}

	private void ChangeState(CustomerState state)
	{
		if (State == state) return;

		State = state;
		
		switch (State)
		{
			case CustomerState.Idle:
				Animator.PlayAnimation("Idle");
				break;
			case CustomerState.Moving:
				Animator.PlayAnimation("Walking");
				break;
			case CustomerState.None:
				break;
			default:
				throw new ArgumentOutOfRangeException();
		}
	}
}

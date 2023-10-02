using Godot;
using System;
using System.Linq;
using Ldjam54.Features.GameplayConfigurations;

public partial class CustomerV2Controller : CharacterBody3D
{
	[Export] public CustomerState InitialState = CustomerState.Idle;
	
	public enum CustomerState
	{
		None, Idle, InQueue, GoingToCapsule, GoingToQueue, LeavingQueue, AdvancingInQueue
	}

	public CustomerState State = CustomerState.None;
    
	public NavigationAgent3D NavigationAgent;
    
	public CharacterAnimationController Animator;
	
	public Reception Reception;
	public Vector3 SpawnLocation;
	public Area3D Clickbox;
	public Sprite3D PatienceBar;

	public CustomerData Data;

	public float CurrentPatience;
	
	public override void _Ready()
	{
		Clickbox = GetNode<Area3D>("clickbox");
		Animator = GetNode<CharacterAnimationController>("model");
		NavigationAgent = GetNode<NavigationAgent3D>("navigator");
		PatienceBar = GetNode<Sprite3D>("PatienceBar");
		PatienceBar.Hide();
		NavigationAgent.NavigationFinished += OnNavigationFinished;

		Clickbox.MouseEntered += ClickboxOnMouseEntered;
		Clickbox.MouseExited += ClickboxOnMouseExited;
		Clickbox.InputEvent += ClickboxOnInputEvent;
		
		GD.Randomize();

		var data = new CustomerData()
		{
			Name = "Test " + GD.Randi(), 
			StayDuration = 5 + GD.RandRange(0, 10),
			PreferredCapsule = CapsuleConfigurations.Capsules[GD.RandRange(0, CapsuleConfigurations.Capsules.Length - 1)] ,
			Patience = 5f + GD.RandRange(5, 10),
		};
		CurrentPatience = data.Patience;
		Initialize(data);
		
		ChangeState(InitialState);
	}

	public void Initialize(CustomerData data)
	{
		Data = data;
	}

	public override void _Process(double delta)
	{
		Navigate(delta);
		HandlePatience(delta);
	}
	
	private void HandlePatience(double delta)
	{
		if (State is not (CustomerState.InQueue or CustomerState.AdvancingInQueue)) return;
		
		CurrentPatience -= (float)delta;
		UpdatePatienceBar();
		if (!(CurrentPatience < 0)) return;
	
		ChangeState(CustomerState.LeavingQueue);
		
		var spawn = GameManager.Instance.LevelManager.CustomerSpawnPoints[GD.Randi() % GameManager.Instance.LevelManager.CustomerSpawnPoints.Length];

		SpawnLocation = spawn.GlobalPosition;

		NavigationAgent.TargetPosition = SpawnLocation;
	}

	public void UpdatePatienceBar()
	{
		if (CurrentPatience < 0)
		{
			PatienceBar.Hide();
			return;
		}

		var remainingPatience = CurrentPatience / Data.Patience;
		PatienceBar.Show();
		PatienceBar.Scale = new Vector3(remainingPatience, 1,1);
		var green = 255 * remainingPatience;
		var red = 255 - green;
		PatienceBar.Modulate = Color.Color8((byte)(red),(byte)(green) , 0, 255);
		
		
	}
	
	public void ChangeState(CustomerState state)
	{
		if (State == state) return;

		State = state;
		
		GD.Print("Changing state to " + state);
		
		switch (State)
		{
			case CustomerState.Idle:
				Animator.PlayAnimation("Idle");
				break;
			case CustomerState.InQueue:
				Animator.PlayAnimation("Idle");
				break;
			case CustomerState.GoingToQueue:
				Animator.PlayAnimation("Walking");
				break;
			case CustomerState.GoingToCapsule:
				Reception.ExitQueue(this);
				Animator.PlayAnimation("Walking");
				break;
			case CustomerState.LeavingQueue:
				Reception.ExitQueue(this);
				Animator.PlayAnimation("Walking");
				break;
			case CustomerState.AdvancingInQueue:
				Animator.PlayAnimation("Walking");
				break;
			case CustomerState.None:
				break;
			default:
				throw new ArgumentOutOfRangeException();
		}
	}

	private void OnNavigationFinished()
	{
		if (CurrentPatience < 0 && NavigationAgent.TargetPosition == SpawnLocation)
		{
			QueueFree();
		}
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
	
	private void Navigate(double delta)
	{
		if (NavigationAgent.IsNavigationFinished())
		{
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
	}
}

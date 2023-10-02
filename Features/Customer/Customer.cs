using Godot;
using System;
using Ldjam54.Features.Customer;


public partial class Customer : CustomerNavigator
{
	
	enum CustomerStatus
	{
		Idle,
		GoingToReception,
		WaitingInQueue,
		GoingToPod,
		Sleeping,
		Exiting
	}
	
	private CapsuleController capsule;
	private Reception _reception;
	private CustomerStatus status = CustomerStatus.Idle;

	public MeshInstance3D _modelOutline;

	[Export] public CharacterAnimationController Animator;
	
	public override void _Ready()
	{
		SetGoingToReception();
		
		Animator = GetNode<CharacterAnimationController>("model");
		_reception = GetTree().GetFirstNodeInGroup("Reception") as Reception;
		_modelOutline = GetNode<MeshInstance3D>("CustomerModel/CustomerModelOutline");
		this.InputEvent += OnCustomerClicked;
		base._Ready();
	}

	public override void _Process(double delta)
	{
		switch (status)
		{
			case CustomerStatus.GoingToReception:
				NavigateToReception();
				break;
			
			case CustomerStatus.WaitingInQueue:
				LookAt(_reception.Position);
				break;
		}
		base._Process(delta);
	}
	

	private void NavigateToReception()
	{
		if (_reception != null && status == CustomerStatus.GoingToReception)
		{
			var positionInQueue = _reception.GetLastWaitingPosition();
			
			if (positionInQueue != navigationTarget)
			{
				NavigateTo(positionInQueue);
			}
			
			if(Position.DistanceTo(positionInQueue) < 0.2f)
			{
				SetWaitingInQueue();
			}
		} 
	}
	
	private void OnCustomerClicked(Node camera, InputEvent @event, Vector3 position, Vector3 normal, long shapeidx)
	{
		if (@event is InputEventMouseButton mouseEvent)
		{
			if (mouseEvent.ButtonIndex == MouseButton.Left && mouseEvent.Pressed)
			{
				// _reception.ExitQueue(this);
				// QueueFree();
				// CustomerManager.SelectedCustomer = this;
			}
		}
	}

	private void SetGoingToReception()
	{
		status = CustomerStatus.GoingToReception;
		
		Animator.PlayAnimation("Walking");
	}

	private void SetWaitingInQueue()
	{
		status = CustomerStatus.WaitingInQueue;
		
		// _reception.EnterQueue(this);
		
		Animator.PlayAnimation("Idle");
	}

	private void ShowOutline()
	{
		
	}
}

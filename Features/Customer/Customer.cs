using Godot;
using System;
using Ldjam54.Features.Customer;


public partial class Customer : CustomerNavigator
{
	
	enum CustomerStatus
	{
		GoingToReception,
		WaitingInQueue,
		GoingToPod,
		Sleeping,
		Exiting
	}
	
	private CapsuleController capsule;
	private Reception _reception;
	private CustomerStatus status = CustomerStatus.GoingToReception;

	public MeshInstance3D _modelOutline;
	

	
	public override void _Ready()
	{
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
			
		}
		base._Process(delta);
	}
	

	private void NavigateToReception()
	{
		if (_reception != null && status == CustomerStatus.GoingToReception)
		{
			var positionInQueue = _reception.GetAvailableQueuePosition();
			if (Position != navigationTarget)
			{
				NavigateTo(positionInQueue);
			}
			else
			{
				_reception.EnterQueue(this);
				status = CustomerStatus.WaitingInQueue;
			}
			

		} 
	}
	
	private void OnCustomerClicked(Node camera, InputEvent @event, Vector3 position, Vector3 normal, long shapeidx)
	{
	
		if (@event is InputEventMouseButton mouseEvent)
		{
			if (mouseEvent.ButtonIndex == MouseButton.Left && mouseEvent.Pressed)
			{
				CustomerManager.SelectedCustomer = this;
			}
		}
	}

	private void ShowOutline()
	{
		
	}
	
	
}

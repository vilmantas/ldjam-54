using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class Reception : Node3D
{
	[Export] public Node3D QueueStart;
	
	public List<CustomerV2Controller> customerQueue = new List<CustomerV2Controller>();
	private int lastQueueLength = 0;
	private Vector3 forward;
	public override void _Ready()
	{
		forward = Vector3.Forward.Rotated(Vector3.Up, Rotation.Y);
	}

	public override void _Process(double delta)
	{
		customerQueue = customerQueue.Where(customer => customer is CustomerV2Controller).ToList();
		UpdateCustomerWaitingLocation();
	}

	private void UpdateCustomerWaitingLocation()
	{
		for (int index = 0; index < customerQueue.Count; index++)
		{
			CustomerV2Controller customer = customerQueue[index];

			var navAgent = customer.NavigationAgent;
            
			var waitingPosition = GetWaitingPosition(index);

			if (customer.State == CustomerV2Controller.CustomerState.Idle)
			{
				navAgent.TargetPosition = waitingPosition;
				customer.ChangeState(CustomerV2Controller.CustomerState.GoingToQueue);
				continue;
			}
			
			if (navAgent.TargetPosition == waitingPosition)
			{
				if (navAgent.IsNavigationFinished())
				{
					customer.ChangeState(CustomerV2Controller.CustomerState.InQueue);
				}
				continue;
			}

			if (customer.State == CustomerV2Controller.CustomerState.InQueue)
			{
				if (navAgent.TargetPosition != waitingPosition)
				{
					navAgent.TargetPosition = waitingPosition;
					customer.ChangeState(CustomerV2Controller.CustomerState.AdvancingInQueue);
				}
			}

			if (customer.State == CustomerV2Controller.CustomerState.GoingToQueue)
			{
				navAgent.TargetPosition = waitingPosition;
			}
			
			if (customer.State == CustomerV2Controller.CustomerState.AdvancingInQueue)
			{
				navAgent.TargetPosition = waitingPosition;
			}
		}
	}

	public void EnterQueue(CustomerV2Controller customer)
	{
		customerQueue.Add(customer);
	}

	public void ExitQueue(CustomerV2Controller customer)
	{
		customerQueue.Remove(customer);
	}

	private Vector3 GetWaitingPosition(int position)
	{
		return QueueStart.GlobalPosition + forward * 1.5f * (position + 1);
	}

	public Vector3 GetLastWaitingPosition()
	{
		return QueueStart.GlobalPosition + forward * 1.5f * (customerQueue.Count + 1);
		
	}
}

using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class Reception : Node3D
{
	public List<Customer> customerQueue = new List<Customer>();
	private int lastQueueLength = 0;
	private Vector3 forward;
	public override void _Ready()
	{
		forward = Vector3.Forward.Rotated(Vector3.Up, Rotation.Y);
	}

	public override void _Process(double delta)
	{
		customerQueue = customerQueue.Where(customer => customer is Customer).ToList();
		UpdateCustomerWaitingLocation();
	}

	private void UpdateCustomerWaitingLocation()
	{
		
		for (int index = 0; index < customerQueue.Count; index++)
		{
			Customer customer = customerQueue[index];
			var waitingPosition = GetWaitingPosition(index);
			if (Position != customer.navigationTarget)
			{
				customer.NavigateTo(waitingPosition);
			}
		}
	}

	public void EnterQueue(Customer customer)
	{
		customerQueue.Add(customer);
	}

	public void ExitQueue(Customer customer)
	{
		customerQueue.Remove(customer);
	}

	private Vector3 GetWaitingPosition(int position)
	{
		return forward * 1.5f * (position + 1);
	}

	public Vector3 GetLastWaitingPosition()
	{
		return forward * 1.5f * (customerQueue.Count + 1);
		
	}
}

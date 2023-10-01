using Godot;
using System;
using System.Collections.Generic;

public partial class Reception : Node3D
{
	public List<Customer> customerQueue = new List<Customer>();

	private Vector3 forward;
	public override void _Ready()
	{
		forward = Vector3.Forward.Rotated(Vector3.Up, Rotation.Y);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}

	public void EnterQueue(Customer customer)
	{
		customerQueue.Add(customer);
	}

	public Vector3 GetAvailableQueuePosition()
	{
		return forward * 1.5f * (customerQueue.Count + 1);
	}
}

using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using Godot.Collections;

public partial class CustomerManager : Node
{
	
	Random random = new Random();
	private PackedScene customerScene;
	private Reception _reception;
	private double spawnCustomerSeconds = 2;
	private double currentSpawnCustomerTimer = 5;

	private List<CustomerSpawnPoint> _customerSpawnPoints = new List<CustomerSpawnPoint>();
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		customerScene = (PackedScene)ResourceLoader.Load("res://Features/CustomerV2/customer_v2.tscn");
		_reception = GetTree().GetFirstNodeInGroup("Reception") as Reception;
		setCustomerSpawners();
	}

	public void setCustomerSpawners()
	{
		var customerSpawnPointNodeList = GetTree().GetNodesInGroup("CustomerSpawnPoint");
		for (int i = 0; i < customerSpawnPointNodeList.Count; i++)
		{
			var customerSpawnPointNode = customerSpawnPointNodeList[i] as CustomerSpawnPoint;
			if(customerSpawnPointNode != null)
			{
				_customerSpawnPoints.Add(customerSpawnPointNode);
			}
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		currentSpawnCustomerTimer += delta;
		if (currentSpawnCustomerTimer >= spawnCustomerSeconds)
		{
			spawnCustomer();
			currentSpawnCustomerTimer -= spawnCustomerSeconds;
		}
	}

	public Vector3 getCustomerSpawnLocation()
	{
		return _customerSpawnPoints[random.Next(0, _customerSpawnPoints.Count - 1)].Position;
	}
	
	public void spawnCustomer()
	{
		if (_customerSpawnPoints.Count > 0)
		{
			CustomerV2Controller newCustomer = (CustomerV2Controller)customerScene.Instantiate();
		
			newCustomer.GlobalPosition = getCustomerSpawnLocation();
			newCustomer.Reception = _reception;
			AddChild(newCustomer);
			_reception.EnterQueue(newCustomer);
		}
	}
}

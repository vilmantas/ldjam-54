using Godot;
using System;

public partial class CustomerMenu : Control
{
	[Export] public Button CloseButton;

	public CustomerV2Controller Customer;

	private Label _nameLabel;

	private Label _stayDurationLabel;
	
	private Label _capsuleLabel;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_nameLabel = GetNode<Label>("GridContainer/customer_name");
		_stayDurationLabel = GetNode<Label>("GridContainer/stay_duration/stay_duration_count");
		_capsuleLabel = GetNode<Label>("GridContainer/capsule/capsule_title");
			
		CloseButton.Pressed += OnCloseButtonPressed;
		GameManager.Instance.OnCustomerDeselected += OnCustomerDeselected;
		GameManager.Instance.OnCustomerSelected += OnCustomerSelected;
		Hide();
	}

	private void OnCustomerDeselected()
	{
		GD.Print("Deselect");
		HandleHide();
	}

	private void OnCustomerSelected(CustomerV2Controller customer)
	{
		GD.Print("Select");
		GD.Print(customer);
		Customer = customer;
		UpdateCustomerData();
		Show();
	}

	private void UpdateCustomerData()
	{
		if (Customer == null)
		{
			return;
		}
		GD.Print("Updating");
		GD.Print(Customer.Data.Name);
		_nameLabel.Text = Customer.Data.Name;
		_stayDurationLabel.Text = Customer.Data.StayDuration.ToString();
		_capsuleLabel.Text = Customer.Data.PreferredCapsule.Title;
	}


	private void OnCloseButtonPressed()
	{
		GD.Print("Close");
		GameManager.Instance.DeselectCustomer();
	}
	
	private void HandleHide()
	{
		Customer = null;
		Hide();
	}
}

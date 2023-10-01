using Godot;
using System;
using Ldjam54.Features.Capsule;

public partial class CapsuleController : Node3D
{
	public CapsuleModel Data;

	public AnimationPlayer AnimationPlayer;

	public override void _Process(double delta)
	{
		if (Data is not OccupiedCapsuleModel occupied) return;

		occupied.Tick((float)delta);

		if (occupied.OccupationDone)
		{
			Data = occupied.GetModel();
			
			AnimationPlayer.Play("anim_capsule/open");
		}
	}

	public void Initialize(CapsuleConfiguration configuration, CapsuleNodeController parent)
	{
		Data = new CapsuleModel() {Parent = parent};
		
		if (parent.Capsules.Count == 2)
		{
			GetNode<Node3D>("spawn").QueueFree();
		}
		
		var model = (CapsuleModelController) configuration.CapsulePrefab.Instantiate();
		AddChild(model);
		
		model.OnHitboxClicked += OnHitboxClicked;

		AnimationPlayer = model.AnimationPlayer;
	}

	private void OnHitboxClicked(MouseButton button)
	{
		if (button == MouseButton.Left)
		{
			GameManager.Instance.SelectCapsule(this);
		}
	}

	public void HandleOccupyRequest()
	{
		if (Data is OccupiedCapsuleModel ) return;

		var customer = GetCustomer();

		Data = new OccupiedCapsuleModel(Data, customer.StayTime);
		
		AnimationPlayer.Play("anim_capsule/close");
	}

	public CustomerModel GetCustomer()
	{
		return new CustomerModel()
		{
			Name = "Blynas",
			StayTime = 3f,
		};
	}
}

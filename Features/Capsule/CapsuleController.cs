using System;
using Godot;

public partial class CapsuleController : Node3D
{
	public CapsuleModel Data;

	public AnimationPlayer AnimationPlayer;
	
	public CapsuleConfiguration Configuration;

	public Action<CapsuleController> OnCapsuleOccupied;

	public Action<CapsuleController> OnCapsuleFreed;

	public override void _Process(double delta)
	{
		if (Data is not OccupiedCapsuleModel occupied) return;

		occupied.Tick((float)delta);

		if (occupied.OccupationDone)
		{
			GD.Print("Stay ended.");
			
			Data = occupied.GetModel();
			
			AnimationPlayer.Play("anim_capsule/open");
			
			OnCapsuleFreed?.Invoke(this);
		}
	}

	public void Initialize(CapsuleConfiguration configuration, CapsuleNodeController parent)
	{
		Data = new CapsuleModel() {Parent = parent};

		Configuration = configuration;
        
		if (parent.Capsules.Count == 2)
		{
			GetNode<Node3D>("spawn").QueueFree();
		}
		
		var model = (CapsuleModelController) Configuration.CapsulePrefab.Instantiate();
		AddChild(model);
		
		model.OnHitboxClicked += OnHitboxClicked;
		model.OnMouseEntered += OnMouseEntered;
		model.OnMouseExited += OnMouseExited;

		AnimationPlayer = model.AnimationPlayer;
	}

	private void OnMouseExited()
	{
		GameManager.Instance.HideTooltip();
	}

	private void OnMouseEntered()
	{
		GameManager.Instance.ShowTooltipText(Configuration.Title);
	}

	private void OnHitboxClicked(MouseButton button)
	{
		if (button == MouseButton.Left)
		{
			GameManager.Instance.SelectCapsule(this);
		}
	}

	public bool HandleOccupyRequest(CustomerData customer)
	{
		if (Data is OccupiedCapsuleModel) return false;

		GD.Print($"Customer {customer.Name} occupied capsule. Staying for: {customer.StayDuration} seconds.");
        
		Data = new OccupiedCapsuleModel(Data, customer, customer.StayDuration);
		
		AnimationPlayer.Play("anim_capsule/close");
		
		OnCapsuleOccupied?.Invoke(this);

		return true;
	}
}

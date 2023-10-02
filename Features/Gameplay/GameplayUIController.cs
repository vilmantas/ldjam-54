using Godot;
using System;

public partial class GameplayUIController : Node
{
	[Export] public Label TotalCapsulesLabel;
	[Export] public Label FreeCapsulesLabel;
	[Export] public Label OccupiedCapsulesLabel;

	public override void _Ready()
	{
		var gameplay = GameManager.Instance.GameplayManager;
		
		gameplay.OnTotalsUpdated += OnTotalsUpdated;
		
		OnTotalsUpdated();
	}

	private void OnTotalsUpdated()
	{
		TotalCapsulesLabel.Text = GameManager.Instance.GameplayManager.TotalCapsulesCount.ToString();
		FreeCapsulesLabel.Text = GameManager.Instance.GameplayManager.FreeCapsules.ToString();
		OccupiedCapsulesLabel.Text = GameManager.Instance.GameplayManager.OccupiedCapsules.ToString();
	}
}

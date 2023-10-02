using Godot;
using System;

public partial class GameplayUIController : Node
{
	[Export] public Label TotalCapsulesLabel;
	[Export] public Label FreeCapsulesLabel;
	[Export] public Label OccupiedCapsulesLabel;
	[Export] public Label MoneyLabel;
	[Export] public Label ComplaintsLabel;
	[Export] public Label GoalLabel;
	
	public override void _Ready()
	{
		var gameplay = GameManager.Instance.GameplayManager;
		
		gameplay.OnTotalsUpdated += OnTotalsUpdated;
		gameplay.OnComplaintReceived += OnComplaintReceived;
		
		MoneyManager.Instance.OnMoneyChanged += OnMoneyChanged;
		
		OnTotalsUpdated();
		OnComplaintReceived();
		OnMoneyChanged(MoneyManager.Instance.CurrentMoney);
		
		GoalLabel.Text = gameplay.CashGoal.ToString("C");
	}

	private void OnComplaintReceived()
	{
		ComplaintsLabel.Text = $"{GameManager.Instance.GameplayManager.Complaints.ToString()}/{GameManager.Instance.GameplayManager.ComplaintMax}";
	}

	private void OnMoneyChanged(int obj)
	{
		MoneyLabel.Text = MoneyManager.Instance.CurrentMoney.ToString("C");
	}

	private void OnTotalsUpdated()
	{
		TotalCapsulesLabel.Text = GameManager.Instance.GameplayManager.TotalCapsulesCount.ToString();
		FreeCapsulesLabel.Text = GameManager.Instance.GameplayManager.FreeCapsules.ToString();
		OccupiedCapsulesLabel.Text = GameManager.Instance.GameplayManager.OccupiedCapsules.ToString();
	}
}

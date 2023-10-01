using Godot;
using System;

public partial class MoneyManager : Node
{
	public static MoneyManager Instance;
	
	public int StartingMoney = 5000;
	
	public int CurrentMoney { get; private set; }
	
	public event Action<int> OnMoneyChanged;
	
	public override void _Ready()
	{
		Instance = this;
		CurrentMoney = StartingMoney;
	}

	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("give_cash"))
		{
			CurrentMoney += 100;
			OnMoneyChanged?.Invoke(100);
		}

		if (Input.IsActionJustPressed("remove_cash"))
		{
			CurrentMoney -= 100;
			OnMoneyChanged?.Invoke(100);
		}
	}

	public void AddMoney(int amount)
	{
		CurrentMoney += amount;
		OnMoneyChanged?.Invoke(amount);
	}
	
	public void RemoveMoney(int amount)
	{
		CurrentMoney -= amount;
		OnMoneyChanged?.Invoke(-amount);
	}
}

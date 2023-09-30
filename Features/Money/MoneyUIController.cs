using Godot;
using System;

public partial class MoneyUIController : Node
{
    [Export] public RichTextLabel MoneyLabel;
    
public override void _Ready()
    {
        MoneyManager.Instance.OnMoneyChanged += OnMoneyChanged;
        
        OnMoneyChanged(MoneyManager.Instance.CurrentMoney);
    }

    private void OnMoneyChanged(int change)
    {
        MoneyLabel.Text = MoneyManager.Instance.CurrentMoney.ToString("C");
    }
}

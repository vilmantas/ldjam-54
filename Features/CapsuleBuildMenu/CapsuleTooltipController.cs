using Godot;
using System;

public partial class CapsuleTooltipController : Control
{
    [Export] public RichTextLabel Title;
    [Export] public RichTextLabel BuildCost;
    [Export] public RichTextLabel BookingCost;
    [Export] public RichTextLabel CostPerHour;
    
    public void Initialize(CapsuleConfiguration configuration)
    {
        Title.Text = configuration.Title;
        BuildCost.Text = configuration.BuildCost.ToString("C");
        BookingCost.Text = configuration.BookingCost.ToString("C");
        CostPerHour.Text = configuration.CostPerHour.ToString("C");
    }

    public override void _Process(double delta)
    {
        Position = GetGlobalMousePosition() + new Vector2(10, 10);
        GlobalPosition = Position;
    }
}

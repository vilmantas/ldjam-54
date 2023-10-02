using Godot;
using System;

public partial class CapsuleTooltipController : Control
{
    [Export] public RichTextLabel Title;
    [Export] public RichTextLabel Cost;
    
    public void Initialize(CapsuleConfiguration configuration)
    {
        Title.Text = configuration.Title;
        Cost.Text = configuration.BuildCost.ToString("C");
    }

    public override void _Process(double delta)
    {
        GlobalPosition = GetGlobalMousePosition();
    }
}

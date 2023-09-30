using Godot;
using System;
using Ldjam54.Features.Capsule;

public partial class CapsuleTooltipController : Control
{
    [Export] public RichTextLabel Title;
    [Export] public RichTextLabel Cost;
    
    public void Initialize(CapsuleConfiguration configuration)
    {
        Title.Text = configuration.Title;
        Cost.Text = configuration.Cost.ToString("C");
    }

    public override void _Process(double delta)
    {
        GlobalPosition = GetGlobalMousePosition();
    }
}

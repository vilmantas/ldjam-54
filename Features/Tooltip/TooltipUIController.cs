using Godot;
using System;

public partial class TooltipUIController : Control
{
	[Export] public RichTextLabel TooltipLabel;
    
	public override void _Ready()
	{
		GameManager.Instance.Tooltip = this;
		
		Hide();
	}

	public override void _Process(double delta)
	{
		var pos = GetGlobalMousePosition();
		pos.X += 15;
		pos.Y += 15;
		Position = pos;
	}

	public void Initialize(string text)
	{
		Show();
		TooltipLabel.Text = text;
	}

	public void HideDisplay()
	{
		Hide();
		TooltipLabel.Text = string.Empty;
	}
}

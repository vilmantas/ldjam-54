using Godot;
using System;

public partial class BuildMenuItem : Button
{
	[Export] public string title;

	public override void _Ready()
	{
		this.Pressed += OnPressed;
	}

	private void OnPressed()
	{
		GameManager.Instance.SetCapsuleConfiguration(title);
	}
}

using Godot;
using System;

public partial class BuildMenuUIController : Control
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GameManager.Instance.OnBuildModeChanged += OnBuildModeChanged;
		
		OnBuildModeChanged(GameManager.Instance.BuildModeActive);
	}

	private void OnBuildModeChanged(bool obj)
	{
		if (obj)
		{
			this.Show();
		}
		else
		{
			this.Hide();
		}
	}
}

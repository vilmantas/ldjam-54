using Godot;
using System;

public partial class BuildMenuUIController : Control
{
	[Export] public CapsuleTooltipController CapsuleTooltip;
	
	[Export] public BuildMenuItemUIController[] BuildMenuItems;
	
	public override void _Ready()
	{
		GameManager.Instance.OnBuildModeChanged += OnBuildModeChanged;
		
		OnBuildModeChanged(GameManager.Instance.BuildModeActive);
		
		CapsuleTooltip.Hide();

		foreach (var menuItem in BuildMenuItems)
		{
			menuItem.OnMouse += OnMenuItemMouseEntered;
			menuItem.OnMouseExit += OnMenuItemMouseExited;
		}
	}

	private void OnMenuItemMouseExited(string obj)
	{
		CapsuleTooltip.Hide();
	}

	private void OnMenuItemMouseEntered(string title)
	{
		var configuration = GameManager.Instance.GetCapsuleConfiguration(title);
		CapsuleTooltip.Initialize(configuration);
		
		CapsuleTooltip.Show();
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

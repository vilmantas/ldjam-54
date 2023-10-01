using Godot;
using System;

public partial class BuildMenuUIController : Control
{
	[Export] public CapsuleTooltipController CapsuleTooltip;
	
	[Export] public BuildMenuItemUIController[] BuildMenuItems;
	
	[Export] public Button CloseButton;

	public CapsuleSpawnController Spawn;
	
	public override void _Ready()
	{
		CapsuleTooltip.Hide();
		
		CloseButton.Pressed += OnCloseButtonPressed;

		foreach (var menuItem in BuildMenuItems)
		{
			menuItem.OnMouse += OnMenuItemMouseEntered;
			menuItem.OnMouseExit += OnMenuItemMouseExited;
			menuItem.Pressed += MenuItemOnPressed;
		}
		
		GameManager.Instance.OnSpawnSelected += Initialize;
		
		Hide();
	}

	private void MenuItemOnPressed()
	{
		GameManager.Instance.BuildCapsule();
		
		Hide();
	}

	public void Initialize(CapsuleSpawnController controller)
	{
		Position = GetGlobalMousePosition();
        
		this.Show();
	}

	private void OnCloseButtonPressed()
	{
		Hide();
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
}

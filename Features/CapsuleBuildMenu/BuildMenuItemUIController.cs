using Godot;
using System;

public partial class BuildMenuItemUIController : Button
{
	[Export] public string title;
	
	public Action<string> OnMouse;
	public Action<string> OnMouseExit;

	public override void _Ready()
	{
		this.Pressed += OnPressed;
		
		this.MouseEntered += OnMouseEntered;
		this.MouseExited += OnMouseExited;
	}

	private void OnMouseExited()
	{
		OnMouseExit?.Invoke(title);
	}

	
	
	private void OnMouseEntered()
	{
		OnMouse?.Invoke(title);
	}

	private void OnPressed()
	{
		GameManager.Instance.SetCapsuleConfiguration(title);
	}
}

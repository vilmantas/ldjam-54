using Godot;
using System;

public partial class BuildMenuItem : Button
{
	[Export] public PackedScene CapsulePrefab;

	public override void _Ready()
	{
		this.Pressed += OnPressed;
	}

	private void OnPressed()
	{
		GameManager.Instance.SetCapsulePrefab(CapsulePrefab);
	}
}

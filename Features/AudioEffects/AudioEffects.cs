using Godot;
using System;

public partial class AudioEffects : Node
{
	public static AudioEffects Instance;
	public static AudioStreamPlayer ClickSound;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Instance = this;
		ClickSound = GetNode<AudioStreamPlayer>("ClickSound");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public static void Click()
	{
		if (AudioEffects.Instance != null)
		{
			ClickSound.Play();
		}
	}
}

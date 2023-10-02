using Godot;
using System;

public partial class BuildingManager : Node
{
	public static BuildingManager Instance;
	
	public CapsuleConfiguration CapsuleConfiguration;

	public Action<CapsuleConfiguration> OnConfigurationSelected;
    
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Instance = this;
	}
	
	public void SetConfiguration(CapsuleConfiguration configuration)
	{
		CapsuleConfiguration = configuration;
		OnConfigurationSelected?.Invoke(configuration);
	}
}

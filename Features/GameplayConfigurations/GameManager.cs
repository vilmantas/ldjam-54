using Godot;
using System;
using System.Linq;
using Ldjam54.Features.Capsule;
using Ldjam54.Features.GameplayConfigurations;

public partial class GameManager : Node
{
    public static GameManager Instance;

    public static PackedScene CapsulePrefab =
        ResourceLoader.Load<PackedScene>("res://Features/Capsule/capsule.tscn");
    
    public CapsuleConfiguration SelectedCapsuleConfiguration;

    public static CapsuleConfiguration[] Capsules = CapsuleConfigurations.Capsules;

    public Action<bool> OnBuildModeChanged;

    public bool BuildModeActive = false;
    
    public override void _Ready()
    {
        Instance = this;
    }

    public override void _Process(double delta)
    {
        if (Input.IsActionJustReleased("ui_build_mode_toggle"))
        {
            BuildModeActive = !BuildModeActive;
            OnBuildModeChanged?.Invoke(BuildModeActive);
        }
    }

    public void SetCapsuleConfiguration(string title)
    {
        SelectedCapsuleConfiguration = Capsules.First(x => x.Title == title);
    }
    
    public bool BuildCapsule(Vector3 globalPosition, Node parent)
    {
        GD.Print("BUILDING");

        if (SelectedCapsuleConfiguration == null)
        {
            GD.Print("Capsule not set you monkey");
        }
        else
        {
            var capsule = (CapsuleController)CapsulePrefab.Instantiate();
            GetTree().Root.AddChild(capsule);
            capsule.Initialize(SelectedCapsuleConfiguration, parent);
            capsule.GlobalPosition = globalPosition;

            return true;
        }

        return false;
    }
}

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
    
    public static CapsuleConfiguration[] Capsules = CapsuleConfigurations.Capsules;
    
    // PROPS
    
    public bool BuildModeActive = false;
    
    
    // PLUGS
    
    public Action<bool> OnBuildModeChanged;
    
    public override void _Ready()
    {
        Instance = this;
    }

    public override void _Process(double delta)
    {
        if (!Input.IsActionJustReleased("ui_build_mode_toggle")) return;
        
        BuildModeActive = !BuildModeActive;
        OnBuildModeChanged?.Invoke(BuildModeActive);
    }

    public void SetCapsuleConfiguration(string title)
    {
        BuildingManager.Instance.SetConfiguration(Capsules.First(x => x.Title == title));
    }
    
    public CapsuleConfiguration GetCapsuleConfiguration(string title)
    {
        return Capsules.First(x => x.Title == title);
    }
    
    public bool BuildCapsule(Vector3 globalPosition, Node parent)
    {
        var config = BuildingManager.Instance.CapsuleConfiguration;

        var cash = MoneyManager.Instance.CurrentMoney;
        
        if (config == null)
        {
            GD.Print("Capsule not set you monkey");
            
            return false;
        }

        if (cash < config.Cost)
        {
            GD.Print("Not enough cash you monkey");
            
            return false;
        }
        
        var capsule = (CapsuleController)CapsulePrefab.Instantiate();
        parent.AddChild(capsule);
        capsule.Initialize(config, parent);
        capsule.GlobalPosition = globalPosition;
        
        MoneyManager.Instance.RemoveMoney(config.Cost);

        return true;
    }
}

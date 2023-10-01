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

    public CapsuleController SelectedCapsule;
    
    public CapsuleSpawnController SelectedSpawn;

    public TooltipUIController Tooltip;
    
    // PROPS
    
    public bool BuildModeActive = false;
    
    
    // PLUGS
    
    public Action<CapsuleController> OnCapsuleSelected;
    
    public Action<CapsuleSpawnController> OnSpawnSelected;
    
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
    
    public bool BuildCapsule()
    {
        var spawn = SelectedSpawn;
        
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

        CapsuleNodeController node = spawn.GetParent() as CapsuleNodeController;
        
        var capsule = (CapsuleController)CapsulePrefab.Instantiate();

        if (node == null)
        {
            node = spawn.GetParent<CapsuleController>().GetParent<CapsuleNodeController>();
        }
        
        node.Capsules.Add(capsule);
        node.AddChild(capsule);
        capsule.Initialize(config, node);
        capsule.GlobalPosition = spawn.GlobalPosition;

        SelectedSpawn = null;
        spawn.QueueFree();
        
        MoneyManager.Instance.RemoveMoney(config.Cost);

        return true;
    }
    
    public void SelectCapsule(CapsuleController capsule)
    {
        SelectedCapsule = capsule;
        OnCapsuleSelected?.Invoke(capsule);
    }

    public void DeselectCapsule()
    {
        SelectedCapsule = null;
    }
    
    public void OccupyCapsule(CapsuleController capsule)
    {
        capsule.HandleOccupyRequest();
    }
    
    public void SelectSpawn(CapsuleSpawnController spawnPoint)
    {
        SelectedSpawn = spawnPoint;
        OnSpawnSelected?.Invoke(spawnPoint);
    }

    public void ShowTooltipText(string text)
    {
        if (SelectedCapsule == null && SelectedSpawn == null)
        {
            Tooltip.Initialize(text);    
        }
    }

    public void HideTooltip()
    {
        Tooltip.HideDisplay();
    }
}

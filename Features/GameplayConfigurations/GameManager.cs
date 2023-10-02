using Godot;
using System;
using System.Linq;
using Ldjam54.Features.GameplayConfigurations;

public partial class GameManager : Node
{
    public static GameManager Instance;

    public static PackedScene CapsulePrefab =
        ResourceLoader.Load<PackedScene>("res://Features/Capsule/capsule.tscn");
    
    public static CapsuleConfiguration[] Capsules = CapsuleConfigurations.Capsules;

    public LevelManager LevelManager;

    public GameplayManager GameplayManager;
    
    public CapsuleController SelectedCapsule;
    
    public CapsuleSpawnController SelectedSpawn;
    
    public CustomerV2Controller SelectedCustomer;

    public TooltipUIController Tooltip;
    
    // PLUGS
    
    public Action<CapsuleController> OnCapsuleSelected;

    public Action OnCapsuleDeselected;
    
    public Action<CapsuleSpawnController> OnSpawnSelected;
    
    public Action<CustomerV2Controller> OnCustomerSelected;

    public Action OnCustomerDeselected;
    
    public override void _Ready()
    {
        Instance = this;
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

        if (cash < config.BuildCost)
        {
            GD.Print("Not enough cash you monkey");
            
            return false;
        }

        CapsuleNodeController node = spawn.GetParent() as CapsuleNodeController;
        
        var capsule = (CapsuleController)CapsulePrefab.Instantiate();

        node ??= spawn.GetParent<CapsuleController>().GetParent<CapsuleNodeController>();
        
        node.AddCapsule(capsule);

        capsule.Initialize(config, node);
        capsule.GlobalPosition = spawn.GlobalPosition;

        SelectedSpawn = null;
        spawn.QueueFree();
        
        MoneyManager.Instance.RemoveMoney(config.BuildCost);

        return true;
    }
    
    public void SelectCapsule(CapsuleController capsule)
    {
        
        SelectedCapsule = capsule;
        OnCapsuleSelected?.Invoke(capsule);
        AudioEffects.Click();
    }

    public void DeselectCapsule()
    {
        SelectedCapsule = null;
        OnCapsuleDeselected?.Invoke();
    }
    
    public void OccupyCapsule()
    {
        if (SelectedCustomer == null) return;

        if (!GameplayManager.OccupyCapsule(SelectedCapsule, SelectedCustomer)) return;
        
        DeselectCustomer();
        DeselectCapsule();
    }

    public void SelectSpawn(CapsuleSpawnController spawnPoint)
    {
        SelectedSpawn = spawnPoint;
        OnSpawnSelected?.Invoke(spawnPoint);
    }

    public void DeselectSpawn()
    {
        SelectedSpawn = null;
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
    
    public void SelectCustomer(CustomerV2Controller customer)
    {
        SelectedCustomer = customer;

        OnCustomerSelected?.Invoke(customer);
        AudioEffects.Click();
    }
    
    public void DeselectCustomer()
    {
        SelectedCustomer = null;
        
        OnCustomerDeselected?.Invoke();
    }

    public void GameOver()
    {
        GetTree().ChangeSceneToFile("res://Scenes/game_over.tscn");
    }
    
    public void Victory()
    {
        GetTree().ChangeSceneToFile("res://Scenes/victory.tscn");
    }
}

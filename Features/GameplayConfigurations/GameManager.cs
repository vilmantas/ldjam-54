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
    
    public CapsuleController SelectedCapsule;
    
    public CapsuleSpawnController SelectedSpawn;
    
    public CustomerV2Controller SelectedCustomer;

    public TooltipUIController Tooltip;
    
    // PROPS
    
    public bool BuildModeActive = false;
    
    
    // PLUGS
    
    public Action<CapsuleController> OnCapsuleSelected;

    public Action OnCapsuleDeselected;
    
    public Action<CapsuleSpawnController> OnSpawnSelected;
    
    public Action<CustomerV2Controller> OnCustomerSelected;

    public Action OnCustomerDeselected;
    
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
        OnCapsuleDeselected?.Invoke();
    }
    
    public void OccupyCapsule()
    {
        if (SelectedCustomer == null)
        {
            return;
        }

        var customer = SelectedCustomer;

        var capsule = SelectedCapsule;

        if (SelectedCustomer.Data.PreferredCapsule != SelectedCapsule.Configuration)
        {
            GD.Print("Invalid selection: Customer prefers different capsule.");
            GD.Print("Preferred: " + SelectedCustomer.Data.PreferredCapsule.Title);
            GD.Print("Selected: " + SelectedCapsule.Configuration.Title);
            return;
        }
        
        SelectedCustomer.NavigationAgent.TargetPosition = LevelManager.CapsuleRoomWaypoint.GlobalPosition;

        GetTree().CreateTimer(4f).Timeout += () => TimerOnTimeout(customer, capsule);
        
        capsule.HandleOccupyRequest(SelectedCustomer.Data);
        
        DeselectCustomer();
        DeselectCapsule();
    }

    private void TimerOnTimeout(CustomerV2Controller customer, CapsuleController capsule)
    {
        customer.Hide();
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
        GD.Print("Selected customer: " + customer.Data.Name);
        GD.Print("Preferred capsule: " + customer.Data.PreferredCapsule.Title);
        
        SelectedCustomer = customer;

        OnCustomerSelected?.Invoke(customer);
    }
    
    public void DeselectCustomer()
    {
        SelectedCustomer = null;
        
        OnCustomerDeselected?.Invoke();
    }
}

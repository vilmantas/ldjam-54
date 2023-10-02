using System;
using System.Linq;
using Godot;

public partial class GameplayManager : Node3D
{
    public CapsuleNodeController[] Nodes;

    public int TotalCapsulesCount = 0;
    public int FreeCapsules = 0;
    public int OccupiedCapsules = 0;
    
    public Action OnTotalsUpdated;
    
    public override void _Ready()
    {
        GameManager.Instance.GameplayManager = this;
        
        Nodes = GetTree().GetNodesInGroup("capsule_node").Cast<CapsuleNodeController>().ToArray();

        foreach (var node in Nodes)
        {
            TotalCapsulesCount += node.Capsules.Count;
            node.OnCapsuleAdded += OnCapsuleAdded;
        }
    }

    public bool OccupyCapsule(CapsuleController capsule, CustomerV2Controller customer)
    {
        if (customer.Data.PreferredCapsule != capsule.Configuration)
        {
            GD.Print("Invalid selection: Customer prefers different capsule.");
            GD.Print("Preferred: " + customer.Data.PreferredCapsule.Title);
            GD.Print("Selected: " + capsule.Configuration.Title);
            return false;
        }
        
        var result = capsule.HandleOccupyRequest(customer.Data);

        if (!result) return false;
        
        customer.NavigationAgent.TargetPosition = GameManager.Instance.LevelManager.CapsuleRoomWaypoint.GlobalPosition;

        GetTree().CreateTimer(4f).Timeout += () => TimerOnTimeout(customer, capsule);
        
        MoneyManager.Instance.AddMoney(customer.Data.PreferredCapsule.BookingCost + customer.Data.PreferredCapsule.CostPerHour * customer.Data.StayDuration);

        return true;
    }
    
    private void TimerOnTimeout(CustomerV2Controller customer, CapsuleController capsule)
    {
        customer.Hide();
    }
    
    private void OnCapsuleAdded(CapsuleController obj)
    {
        obj.OnCapsuleOccupied += UpdateTotals;
        obj.OnCapsuleFreed += UpdateTotals;
        
        UpdateTotals(obj);
    }

    private void UpdateTotals(CapsuleController obj)
    {
        OccupiedCapsules = 0;
        FreeCapsules = 0;
        TotalCapsulesCount = 0;
        
        foreach (var node in Nodes)
        {
            foreach (var capsule in node.Capsules)
            {
                TotalCapsulesCount++;
                
                if (capsule.Data is OccupiedCapsuleModel)
                {
                    OccupiedCapsules++;
                }
                else
                {
                    FreeCapsules++;
                }
            }
        }
        
        OnTotalsUpdated?.Invoke();
    }
}
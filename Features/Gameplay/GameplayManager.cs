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
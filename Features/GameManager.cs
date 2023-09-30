using Godot;
using System;

public partial class GameManager : Node
{
    public static GameManager Instance;

    public PackedScene CapsulePrefab;
    
    public override void _Ready()
    {
        Instance = this;
    }

    public void SetCapsulePrefab(PackedScene prefab)
    {
        CapsulePrefab = prefab;
    }
    
    public void BuildCapsule(Vector3 globalPosition)
    {
        GD.Print("BUILDING");

        if (CapsulePrefab == null)
        {
            GD.Print("Capsule not set you monkey");
        }
        else
        {
            var capsule = (CapsuleController) CapsulePrefab.Instantiate();
            GetTree().Root.AddChild(capsule);
            capsule.GlobalPosition = globalPosition;
        }
    }
}

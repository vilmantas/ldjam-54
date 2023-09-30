using Godot;
using System;

public partial class CapsuleBuildMenuController : Node
{
    public Node3D SpawnPoint;
    
    public override void _Ready()
    {
        SpawnPoint = GetNode<Node3D>("spawn");
    }
}

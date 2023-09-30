using Godot;

namespace Ldjam54.Features.Capsule;

public class CapsuleModel
{
    public Node Parent;
    
    public bool IsOccupied = false;

    public float TimeOccupied = 0f;

    public float OccupiedFor = 0f;
}
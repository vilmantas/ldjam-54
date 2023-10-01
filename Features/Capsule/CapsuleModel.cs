using System;
using Godot;

public class CapsuleModel
{
    public Node Parent;
}

public class OccupiedCapsuleModel : CapsuleModel
{
    public CustomerModel Occupier;

    public float TimeOccupiedPassed = 0f;

    public float OccupationDuration = 0f;
    
    public bool OccupationDone => TimeOccupiedPassed >= OccupationDuration;
    
    public OccupiedCapsuleModel(CapsuleModel model, float duration)
    {
        Parent = model.Parent;
        
        OccupationDuration = duration;
    }

    public void Tick(float delta)
    {
        TimeOccupiedPassed = Mathf.Min(TimeOccupiedPassed + delta, OccupationDuration);
    }
    
    public CapsuleModel GetModel()
    {
        return new CapsuleModel() {Parent = Parent};
    }
}
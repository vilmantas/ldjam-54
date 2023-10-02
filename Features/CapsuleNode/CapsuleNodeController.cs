using Godot;
using System;
using System.Collections.Generic;

public partial class CapsuleNodeController : Node
{
    public List<CapsuleController> Capsules = new ();
    
    public Action<CapsuleController> OnCapsuleAdded;

    public void AddCapsule(CapsuleController capsule)
    {
        Capsules.Add(capsule);
        AddChild(capsule);
        
        OnCapsuleAdded?.Invoke(capsule);
    }
}

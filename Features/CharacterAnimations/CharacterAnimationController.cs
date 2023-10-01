using Godot;
using System;

public partial class CharacterAnimationController : Node3D
{
    [Export] public AnimationTree Animator;
    
    public override void _Ready()
    {
        Animator = GetNode<AnimationTree>("AnimationTree");
    }

    public void PlayAnimation(string animationName)
    {
        Animator.Set("parameters/Transition/transition_request", animationName);
    }
}

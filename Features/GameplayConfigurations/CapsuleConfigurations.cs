using Godot;
using Ldjam54.Features.Capsule;

namespace Ldjam54.Features.GameplayConfigurations;

public static class CapsuleConfigurations
{
    private static string CapsulesFolder = "res://Models/Capsules";
    
    public static readonly CapsuleConfiguration BASIC_CAPSULE = new()
    {
        CapsulePrefab = ResourceLoader.Load<PackedScene>($"{CapsulesFolder}/model_capsule_basic.tscn"),
        Title = "Basic Capsule",
        Cost = 100,
    };
    
    public static readonly CapsuleConfiguration NICE_CAPSULE = new()
    {
        CapsulePrefab = ResourceLoader.Load<PackedScene>($"{CapsulesFolder}/model_capsule_nice.tscn"),
        Title = "Nice Capsule",
        Cost = 200,
    };
    
    public static readonly CapsuleConfiguration PRO_CAPSULE = new()
    {
        CapsulePrefab = ResourceLoader.Load<PackedScene>($"{CapsulesFolder}/model_capsule_pro.tscn"),
        Title = "Pro Capsule",
        Cost = 200,
    };
    
    public static readonly CapsuleConfiguration[] Capsules = new[]
    {
        BASIC_CAPSULE,
        NICE_CAPSULE,
        PRO_CAPSULE
    };
}
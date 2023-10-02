using Godot;

namespace Ldjam54.Features.GameplayConfigurations;

public static class CapsuleConfigurations
{
    private static string CapsulesFolder = "res://Models/Capsules";
    
    public static readonly CapsuleConfiguration BASIC_CAPSULE = new()
    {
        CapsulePrefab = ResourceLoader.Load<PackedScene>($"{CapsulesFolder}/model_capsule_basic.tscn"),
        Title = "Basic Capsule",
        BuildCost = 100,
        BookingCost = 2,
        CostPerHour = 4
    };
    
    public static readonly CapsuleConfiguration NICE_CAPSULE = new()
    {
        CapsulePrefab = ResourceLoader.Load<PackedScene>($"{CapsulesFolder}/model_capsule_nice.tscn"),
        Title = "Nice Capsule",
        BuildCost = 200,
        BookingCost = 4,
        CostPerHour = 8
    };
    
    public static readonly CapsuleConfiguration PRO_CAPSULE = new()
    {
        CapsulePrefab = ResourceLoader.Load<PackedScene>($"{CapsulesFolder}/model_capsule_pro.tscn"),
        Title = "Pro Capsule",
        BuildCost = 300,
        BookingCost = 4,
        CostPerHour = 12
    };
    
    public static readonly CapsuleConfiguration[] Capsules = new[]
    {
        BASIC_CAPSULE,
        NICE_CAPSULE,
        PRO_CAPSULE
    };
}
using System.Reflection;

namespace FoodFlow.Modules.Identity.Domain;

/// <summary>
/// Marker used to locate the Parking.Core assembly for handler/validator discovery.
/// </summary>
public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}

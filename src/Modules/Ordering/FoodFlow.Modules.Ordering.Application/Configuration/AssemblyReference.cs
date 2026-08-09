using System.Reflection;

namespace FoodFlow.Modules.Ordering.Application.Configuration;

/// <summary>
/// Marker used to locate the Parking.Core assembly for handler/validator discovery.
/// </summary>
public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}

using BepInEx;
using BepInEx.Logging;
using BepInEx.NET.Common;
using BepInExResoniteShim;
using FrooxEngine;

namespace BepisResoniteHooks;

[ResonitePlugin(PluginMetadata.GUID, PluginMetadata.NAME, PluginMetadata.VERSION, PluginMetadata.AUTHORS, PluginMetadata.REPOSITORY_URL)]
[BepInDependency(BepInExResoniteShim.PluginMetadata.GUID, BepInDependency.DependencyFlags.HardDependency)]
public class ResoniteHooks : BasePlugin
{
    internal static new ManualLogSource Log = null!;

    public static Action<Engine>? OnEngineReady;

    public override void Load()
    {
        Log = base.Log;
        HarmonyInstance.PatchAll();
        Log.LogInfo($"Plugin {PluginMetadata.GUID} is loaded!");
    }
}
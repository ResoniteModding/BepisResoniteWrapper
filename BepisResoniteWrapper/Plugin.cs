using BepInEx;
using BepInEx.Logging;
using BepInEx.NET.Common;
using BepInExResoniteShim;
using BepisResoniteWrapper.Hooks;

namespace BepisResoniteWrapper;

[ResonitePlugin(PluginMetadata.GUID, PluginMetadata.NAME, PluginMetadata.VERSION, PluginMetadata.AUTHORS, PluginMetadata.REPOSITORY_URL)]
[BepInDependency(BepInExResoniteShim.PluginMetadata.GUID, BepInDependency.DependencyFlags.HardDependency)]
class Plugin : BasePlugin
{
    internal static new ManualLogSource Log = null!;

    public override void Load()
    {
        Log = base.Log;
        RunPatches();
        Log.LogInfo($"Plugin {PluginMetadata.GUID} is loaded!");
    }

    void RunPatches()
    {
        HarmonyInstance.SafePatchCategory(nameof(EngineReadyHook));
    }
}
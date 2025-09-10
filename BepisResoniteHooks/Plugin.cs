using System.Reflection;
using BepInEx;
using BepInEx.Logging;
using BepInEx.NET.Common;
using BepInExResoniteShim;
using FrooxEngine;
using HarmonyLib;

namespace BepisResoniteHooks;

[ResonitePlugin(PluginMetadata.GUID, PluginMetadata.NAME, PluginMetadata.VERSION, PluginMetadata.AUTHORS, PluginMetadata.REPOSITORY_URL)]
[BepInDependency(BepInExResoniteShim.PluginMetadata.GUID, BepInDependency.DependencyFlags.HardDependency)]
public class ResoniteHooks : BasePlugin
{
    internal static new ManualLogSource Log;
    public static event Action<Engine>? OnEngineReady;

    public override void Load()
    {
        Log = base.Log;
        HarmonyInstance.PatchAll();
        Log.LogInfo($"Plugin {PluginMetadata.GUID} is loaded!");
    }

    [HarmonyPatch]
    public class EngineReadyPatch
    {
        static MethodInfo? TargetMethod()
        {
            try
            {
                return AccessTools.Method(typeof(Engine), "SetReady");
            }
            catch (Exception ex)
            {
                Log.LogWarning(
                    $"Engine.SetReady not found, OnEngineReady will not fire. ({ex.Message})");
                return null;
            }
        }

        static void Postfix(Engine __instance)
        {
            Log.LogInfo("Engine initialization finished, firing OnEngineReady event");
            OnEngineReady.SafeInvokeAll(__instance, Log);
        }
    }
}

public static class Extensions
{
    public static void SafeInvokeAll<T>(this Action<T>? evt, T arg, ManualLogSource logger)
    {
        if (evt == null) return;

        foreach (var handler in evt.GetInvocationList())
        {
            try
            {
                ((Action<T>)handler)(arg);
            }
            catch (Exception ex)
            {
                logger.LogError(
                    $"Exception in {evt.GetType().Name} subscriber {handler.Method.DeclaringType?.FullName}.{handler.Method.Name}: {ex}");
            }
        }
    }
}
using System.Reflection;
using BepInEx.Logging;
using FrooxEngine;
using HarmonyLib;

namespace BepisResoniteHooks.Hooks;

[HarmonyPatch]
public class EngineReadyHook
{
    private static ManualLogSource Log => ResoniteHooks.Log;

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
        ResoniteHooks.OnEngineReady.SafeInvokeAll(__instance);
    }
}
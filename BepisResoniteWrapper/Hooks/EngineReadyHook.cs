using FrooxEngine;
using HarmonyLib;

namespace BepisResoniteWrapper.Hooks;

using static Plugin;
using static ResoniteHooks;

[HarmonyPatchCategory(nameof(EngineReadyHook))]
[HarmonyPatch(typeof(Engine), "SetReady")]
class EngineReadyHook
{
    static void Postfix(Engine __instance)
    {
        if (OnEngineReadySubs) Log.LogInfo("Engine initialization finished, firing OnEngineReady event");
        RunOnEngineReady();
    }
}
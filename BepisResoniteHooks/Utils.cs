using HarmonyLib;

namespace BepisResoniteHooks;

internal static class Utils
{
    public static void SafeInvokeAll(this Action? evt)
    {
        if (evt == null) return;

        foreach (var handler in evt.GetInvocationList())
        {
            try
            {
                ((Action)handler).Invoke();
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"Exception in {evt.GetType().Name} subscriber {handler.Method.DeclaringType?.FullName}.{handler.Method.Name}: {ex}");
            }
        }
    }
    public static void SafePatchCategory(this Harmony instance, string CategoryName)
    {
        try
        {
            instance.PatchCategory(CategoryName);
        }
        catch (Exception e)
        {
            Plugin.Log.LogError($"Failed to patch {CategoryName}. it's hook will not fire");
        }
    }
}
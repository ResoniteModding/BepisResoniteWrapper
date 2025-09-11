using BepInEx.Logging;

namespace BepisResoniteHooks;

public static class Utils
{
    public static void SafeInvokeAll<T>(this Action<T>? evt, T arg)
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
                ResoniteHooks.Log.LogError($"Exception in {evt.GetType().Name} subscriber {handler.Method.DeclaringType?.FullName}.{handler.Method.Name}: {ex}");
            }
        }
    }
}
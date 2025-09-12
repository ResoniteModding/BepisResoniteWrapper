namespace BepisResoniteWrapper;

static public class ResoniteHooks
{
    public static event Action? OnEngineReady;
    internal static bool OnEngineReadySubs => OnEngineReady != null;
    internal static void RunOnEngineReady() => OnEngineReady.SafeInvokeAll();
}

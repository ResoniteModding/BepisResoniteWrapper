# BepisResoniteWrapper
[![Thunderstore Badge](https://modding.resonite.net/assets/available-on-thunderstore.svg)](https://thunderstore.io/c/resonite/)

A [Resonite](https://resonite.com/) BepInEx library (not a standalone mod) that provides commonly-used hooks and events for other BepInEx mods. This library simplifies mod development by offering ready-to-use events like `OnEngineReady`.

## Installation (Manual)
1. Install [BepisLoader](https://github.com/ResoniteModding/BepisLoader) for Resonite.
2. Download the latest release ZIP file (e.g., `ResoniteModding-BepisResoniteWrapper-1.0.0.zip`) from the [Releases](https://github.com/ResoniteModding/BepisResoniteWrapper/releases) page.
3. Extract the ZIP and copy the `plugins` folder to your BepInEx folder in your Resonite installation directory:
   - **Default location:** `C:\Program Files (x86)\Steam\steamapps\common\Resonite\BepInEx\`
4. Start the game. If you want to verify that the mod is working you can check your BepInEx logs.

## Usage for Developers

### Adding BepisResoniteWrapper to Your Mod

1. Add the NuGet package reference to your mod's `.csproj` file:

```xml
<PackageReference Include="ResoniteModding.BepisResoniteWrapper" Version="1.0.*" />
```

2. Add it as a dependency in your `thunderstore.toml`:

```toml
[package.dependencies]
ResoniteModding-BepisResoniteWrapper = "1.0.0"
```

### Using the Events

Subscribe to events in your BepInEx plugin:

```csharp
using BepisResoniteWrapper;

public class MyMod : BasePlugin
{
    public override void Load()
    {
        // Subscribe to the OnEngineReady event
        ResoniteHooks.OnEngineReady += OnEngineReady;
    }
    
    private void OnEngineReady()
    {
        // The Resonite engine is now fully initialized
        // Safe to access FrooxEngine classes and functionality
        Log.LogInfo("Engine is ready with version: " + Engine.Current.VersionString);
    }
}
```

## Available Events

- **`OnEngineReady`** - Fired when the Resonite engine has finished.
  - No parameters - the engine instance can be accessed via `Engine.Current`

## For Library Contributors

To add new hooks to this library:

1. Create a new hook class in the `BepisResoniteWrapper/Hooks/` folder
2. Add the corresponding event to the `ResoniteHooks` class in `Api.cs`
3. Follow the pattern used in `EngineReadyHook.cs`:
   - Use `[HarmonyPatchCategory]` attribute for organization
   - Use `[HarmonyPatch]` to target the method to hook
   - Implement Prefix/Postfix methods as needed
   - Call the appropriate event from `ResoniteHooks`

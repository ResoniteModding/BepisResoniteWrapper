# BepisResoniteHooks
[![Thunderstore Badge](https://modding.resonite.net/assets/available-on-thunderstore.svg)](https://thunderstore.io/c/resonite/)

A [Resonite](https://resonite.com/) BepInEx library (not a standalone mod) that provides commonly-used hooks and events for other BepInEx mods. This library simplifies mod development by offering ready-to-use events like `OnEngineReady`.

## Installation (Manual)
1. Install [BepisLoader](https://github.com/ResoniteModding/BepisLoader) for Resonite.
2. Download the latest release ZIP file (e.g., `ResoniteModding-BepisResoniteHooks-1.0.0.zip`) from the [Releases](https://github.com/ResoniteModding/BepisResoniteHooks/releases) page.
3. Extract the ZIP and copy the `plugins` folder to your BepInEx folder in your Resonite installation directory:
   - **Default location:** `C:\Program Files (x86)\Steam\steamapps\common\Resonite\BepInEx\`
4. Start the game. If you want to verify that the mod is working you can check your BepInEx logs.

## Usage

Add a reference to `BepisResoniteHooks` in your mod project, then subscribe to the events you need:

```csharp
using BepisResoniteHooks;

public class MyMod : BasePlugin
{
    public override void Load()
    {
        ResoniteHooks.OnEngineReady += OnEngineReady;
    }
    
    private void OnEngineReady(Engine engine)
    {
        // Your code here - engine is fully initialized
    }
}
```

## Available Events

- **`OnEngineReady`** - Fired when the Resonite engine has finished initialization and is ready for use

## For Library Contributors

This library is designed to be extended with additional hooks and events as needed by the community. To add new hooks:

1. Create a new hook class in the `Hooks/` folder
2. Add the corresponding event to the `ResoniteHooks` class
3. Follow the existing pattern used in `EngineReadyHook.cs`
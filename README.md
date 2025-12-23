# PartMenuAPI

PartMenuAPI is a tiny, focused compatibility mod that provides a stable hook for other mods to render part-related UI that needs access to the full `Part[] allParts` array during the game's stats-drawing flow.

Forum post: [https://jmnet.one/sfs/forum/index.php?threads/partmenuapi-dependency.16896/](https://jmnet.one/sfs/forum/index.php?threads/partmenuapi-dependency.16896/)

## Why this exists

- The engine's `Part.DrawPartStats(Part[] allParts, StatsMenu drawer, PartDrawSettings settings)` method is called for each part. Many modders need access to the entire `allParts` array (to aggregate, compare or group modules across parts), but the engine `I_PartMenu.Draw` signature does not provide that array.
- `PartMenuAPI` exposes a single interface and a Harmony postfix that invokes `Draw(allParts, drawer, settings)` for components implementing the interface. This avoids every mod writing its own patch and centralises the integration point.

For reference, I_PartMenu.Draw is defined as:

```csharp
namespace SFS.Parts.Modules
{
    public interface I_PartMenu
    {
        void Draw(StatsMenu drawer, PartDrawSettings settings);
    }
}
```

This forces modders to manually find and aggregate `allParts` themselves, which is a hassle as they must check what parts are selected in the current context.

## What it provides

- `PartMenuAPI.IPartMenuAllParts` interface modders implement on a `MonoBehaviour` attached to a `Part` prefab or instance:

```csharp
namespace PartMenuAPI
{
    public interface IPartMenuAllParts
    {
        void Draw(Part representative, Part[] allParts, StatsMenu drawer, PartDrawSettings settings);
    }
}
```

- A small Harmony postfix on `Part.DrawPartStats` which collects implementors and calls `Draw(allParts[0], allParts, drawer, settings)` on components attached to the current `Part` instance.

PS: `allParts[0]` is passed as the `representative` parameter for convenience and to replicate the game's default behaviour, as in the build menu the default behaviour is to show the first selected part as a representative for all others, for example when opening the part stats menu on the right side of the screen.

## Installation

1. Build `PartMenuAPI` into a DLL and place it under `Mods/PartMenuAPI/` (the game's `Loader` will pick it up).
2. Mods that depend on the API should declare the dependency in their `Mod` subclass `Dependencies` property so the Loader ensures `PartMenuAPI` loads first.

## Usage

1. Implement `IPartMenuAllParts` on a `MonoBehaviour` attached to your `Part` prefab or added at runtime.
2. Implement `Draw(Part[] allParts, StatsMenu drawer, PartDrawSettings settings)` and use `drawer` to add UI entries.
3. If your UI should use a particular representative part, ensure the caller places that part at index `0` of `allParts` (the engine uses `allParts[0]` as the representative in many contexts).

Minimal example

```csharp
using PartMenuAPI;
using SFS.Parts;
using SFS.Parts.Modules;
using SFS.UI;

public class MyAggregateModule : MonoBehaviour, IPartMenuAllParts
{
    public void Draw(Part representative, Part[] allParts, StatsMenu drawer, PartDrawSettings settings)
    {
        var total = allParts.Sum(p => GetFuel(p));
        drawer.DrawStat(50, $"Total fuel: {total}");
    }

    private double GetFuel(Part p)
    {
        // read resource module(s) to compute fuel
        return 0.0;
    }
}
```

## Troubleshooting

- `Draw` not called: ensure your component is attached to a `Part` that actually appears in `allParts` for the current stats call and that `PartMenuAPI` is loaded before your mod.

## Versioning and releases

- Keep the API minimal. If adding members, bump the API mod version and document breaking changes. Consumers should declare a minimum required `PartMenuAPI` version in their `Mod.Dependencies`.

## Contributing

- Small PRs are welcome: keep changes focused to the postfix logic, tests/examples and docs.

## License

- GNU General Public License. See LICENSE file for details.

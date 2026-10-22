# 3D Engine Options (C#, Code-First, FOSS, Cross-Platform)

## Goals / Constraints

- Code-first development style (no node/object editor workflow like Unity/Godot)
- C# preferred
- Free and open source
- Desktop targets: macOS, Windows, Linux
- 3D (low-poly) to avoid the challenge of drawing 2D pixel art in multiple orientations

## Options

| Option | Language | License | Code-first? | 3D out of the box | Mac/Win/Linux | Notes |
|---|---|---|---|---|---|---|
| **Raylib (Raylib-cs)** | C# bindings to a C library | zlib | Yes, entirely | Good: models, cameras, animation, shaders | Yes, native libraries come in the NuGet package | Simple API that feels like C, not like idiomatic C# |
| **MonoGame** | C# | MS-PL | Yes | Bare: `BasicEffect`, simple model loading | Yes | You build a lot yourself; skeletal animation is a DIY project |
| **FNA** | C# | MS-PL | Yes | Same as MonoGame | Yes | An XNA reimplementation, so the same 3D limits as MonoGame |
| **Stride** | C# | MIT | Mostly built around its editor, but code-only works (Stride Community Toolkit) | Strong: PBR rendering, physics, full engine | Runtime works on Linux; the editor is Windows-only; Mac support is weak | The most "real engine" option in C#, but it pulls you back toward an editor workflow and is weak on Mac |
| **Silk.NET** | C# | MIT | Yes | None (raw OpenGL/Vulkan bindings) | Yes | Only if you want to write your own engine |
| **Bevy** | Rust | MIT/Apache | Yes (ECS) | Strong | Yes | Fits the code-first style well, but it isn't C# |

Flax and Evergine are C# too, but neither is fully free and open source, so they're left out.

## Recommendation: Start with Raylib (via Raylib-cs)

### Why raylib over MonoGame for 3D

- **Loading models is one call.** `LoadModel("ship.glb")` handles glTF, OBJ, IQM and M3D files, and `LoadModelAnimations` handles skeletal animation. In MonoGame you'd go through the content pipeline and write or borrow your own animation code.
- **No content pipeline.** Drop files in a folder and load them at runtime. Iterating on art is much faster when you're also the one making it.
- **Built-in cameras and drawing helpers.** There's an orbit/free/first-person camera, `DrawGrid`, `DrawCube`, ray picking and collision helpers, so you can prototype with plain shapes before you have any models.
- **Hundreds of small examples,** including lighting, shadows and instancing, and they translate almost line for line into C#.

### Downsides to know about

- The API is C-style: static functions and structs, not an object model. Most people write a thin C# layer on top.
- Lighting isn't automatic. Use the example lighting shaders or write your own. For low-poly, flat or toon shading is easy and looks good.
- There's no physics engine. Pair it with **BepuPhysics v2**, which is written in pure C# and is excellent.
- On macOS it runs on OpenGL, which Apple has deprecated but which still works. MonoGame's cross-platform desktop version is in the same position.

## Making Art Without Being an Artist

- **Free low-poly asset packs:** Kenney.nl, Quaternius, KayKit. Many are CC0 and include rigged, animated characters in glTF.
- **MagicaVoxel:** building with voxels is very approachable if you can't draw, and it exports OBJ files that load straight into raylib.
- **Blender** with low-poly tutorials when you're ready. Export to glTF (`.glb`).
- **Asset Forge:** build models by snapping parts together. It's paid but cheap.

## Suggested First Prototype (when ready)

A Raylib-cs .NET console app with:

- A free-look camera
- A ground grid
- A CC0 animated character loaded from `.glb` that you can walk around with WASD
- Simple directional lighting

That covers model loading, animation, input and shaders. If anything there feels painful, you'll know early.

Open question: start with a single file, or with a small structure (game loop, scenes, asset loading) to build on?

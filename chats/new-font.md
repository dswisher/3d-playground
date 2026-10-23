# Plan: Custom Font (Courier Prime) in 3D Playground

Handoff notes for a fresh agent session. Status: **plan only, no code changes made yet.**

## Context

- Repo: `~/git/dswisher/3d-playground` — a learning project for raylib + 3D (C#, .NET 10, `Raylib-cs` 8.1.0).
- The user (Doug) has 40 years of software engineering, a couple of years of hobby gamedev (most recently MonoGame). He's learning raylib.
- **Working style:** the user mostly keeps the agent in plan mode and wants to be *walked through* changes, with explanations. Follow `AGENTS.md`: ask permission before making code changes, no compiler warnings, never run state-modifying `git` commands.
- The user keeps a devlog/cheat-sheet at `devlog/content/posts/2026-10-23-raylib-basics/index.md` (Hugo). **It's the user's post — don't edit it unless asked**; just give him material for it.
- Build settings (`Directory.Build.props`): `Nullable` enabled, `ImplicitUsings` disabled, `AnalysisMode=Recommended`, `EnforceCodeStyleInBuild=true`. So: explicit `using`s, and avoid nullable warnings.
- Files involved:
  - `src/Playground/Playground.csproj`
  - `src/Playground/Program.cs` — `InitWindow`, `SetTargetFPS`, `SetExitKey(Null)`, a main loop, `try/finally` that calls `sceneManager.Shutdown()` then `Raylib.CloseWindow()`.
  - `src/Playground/MenuScene.cs` — `Draw()` uses `Raylib.DrawText(...)` (sizes 32 and 24).
  - `src/Playground/PlaceholderScene.cs` — `Draw()` uses `Raylib.DrawText(...)` (lines ~39-40).
  - `README.md` — no Assets section yet.

## Goal

Use the custom font `courier-prime.ttf` (from `~/git/dswisher/groundwork/assets/fonts/courier-prime.ttf`) for all UI text, and credit the font author in `README.md`.

## Finding: credit link is missing in groundwork

The user believed `~/git/dswisher/groundwork/README.md` had a credit link for the font — **it does not** (its Assets section only lists sprites). Font is only referenced in groundwork's `assets/assets.go` and `internal/font/cache.go`.

Known info (from memory — user should verify):
- **Courier Prime** by Alan Dague-Greene, for Quote-Unquote Apps.
- License: **SIL Open Font License 1.1** (free incl. commercial; keep license/credit with the font).
- https://quoteunquoteapps.com/courierprime/ (also https://fonts.google.com/specimen/Courier+Prime)
- Suggested the user also add the credit to groundwork's README.

## Concepts already explained to the user (good blog material)

1. **Fonts are texture atlases.** `LoadFontEx(path, size, codepoints, count)` rasterizes glyphs once, at one pixel size, into a GPU texture; drawing text blits from that atlas. (Contrast: MonoGame builds `SpriteFont` at content-build time; raylib does it at runtime.)
2. **Load after `InitWindow`** (needs the GL context) and **`UnloadFont` before `CloseWindow`**.
3. **Load size matters.** Text is crispest when drawn at the load size. Strategy: load large (~48px), draw smaller, and `SetTextureFilter(font.Texture, TextureFilter.Bilinear)` for smooth downscaling.
4. **Codepoints choose which glyphs get loaded.** `null` = 95 printable ASCII chars, but with nullable enabled that warns, so build the ASCII 32–126 array explicitly (also documents where to add `—`, `°`, etc. later).
5. **`DrawText` uses the built-in pixel font**; custom fonts use `DrawTextEx(font, text, Vector2 position, float fontSize, float spacing, Color)` (`System.Numerics.Vector2`). `MeasureTextEx` is the layout counterpart.

Verified Raylib-cs 8.1.0 overloads (from the package XML docs):
- `LoadFontEx(string fileName, int fontSize, int[] codepoints, int codepointCount)`
- `LoadFontFromMemory(string fileType, byte[] fileData, int fontSize, int[] codepoints, int codepointCount)` (alternative if embedding as a resource)
- `DrawTextEx(Font, string, Vector2, float, float, Color)`
- `MeasureTextEx(Font, string, float, float)`
- `IsFontValid(Font)`, `UnloadFont(Font)`, `SetTextureFilter(Texture2D, TextureFilter)`

## Proposed steps

1. **Add the asset**
   - Copy the font to `src/Playground/assets/fonts/courier-prime.ttf`.
   - In `Playground.csproj` add:
     ```xml
     <ItemGroup>
       <None Include="assets/**" CopyToOutputDirectory="PreserveNewest" />
     </ItemGroup>
     ```
   - Build the path from `AppContext.BaseDirectory` (e.g. `Path.Combine(AppContext.BaseDirectory, "assets", "fonts", "courier-prime.ttf")`), not a relative path — `dotnet run` and IDEs can use different working directories.

2. **New `src/Playground/Fonts.cs`** — static class with `Load()`, `Unload()`, `Font Main { get; }`.
   - `Load()`: build ASCII 32–126 codepoint array, `LoadFontEx(path, 48, codepoints, codepoints.Length)`, `SetTextureFilter(Main.Texture, TextureFilter.Bilinear)`, check `IsFontValid` and throw with the path if invalid.
   - Match existing file style: copyright header, `namespace Playground { ... }` block style, `internal`.
   - Rationale: simplest; scenes use it without changing `IScene` or constructors. Alternative offered: inject the font into scenes (less global state, more plumbing).

3. **`Program.cs`** — call `Fonts.Load()` after `InitWindow`/`SetTargetFPS`; call `Fonts.Unload()` in `finally` after `sceneManager.Shutdown()` and before `Raylib.CloseWindow()`.

4. **Scenes** — in `MenuScene.Draw` and `PlaceholderScene.Draw`, replace
   `Raylib.DrawText(text, x, y, size, color)` with
   `Raylib.DrawTextEx(Fonts.Main, text, new Vector2(x, y), size, spacing, color)`.
   Built-in font uses spacing `size / 10`; for Courier Prime start with `0` or `1` and tune by eye. Consider a shared spacing constant.

5. **`README.md`** — add `## Assets` → `### Fonts` section (same style as groundwork) with the Courier Prime link + "SIL Open Font License 1.1". Ideally also include the OFL text at `src/Playground/assets/fonts/OFL.txt`.

6. **Devlog** — don't edit; offer the concept list above and the `LoadFontEx` / `DrawTextEx` / `UnloadFont` snippets as a "Custom fonts" section for the user to write.

Finally: `dotnet build` and confirm zero warnings.

## Open questions for the user (ask these first)

1. **Asset location:** `src/Playground/assets/` (recommended) or repo-root `assets/` like groundwork?
2. **Font access:** static `Fonts` class (recommended) or inject the font into scenes?
3. **Pace:** walk through one step at a time (explaining each diff), or apply everything at once?
4. Can the user confirm the Courier Prime credit/license info and preferred link?

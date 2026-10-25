+++
date = '2026-10-23T08:30:00-05:00'
draft = false
title = 'raylib Basics'
slug = 'raylib-basics'
tags = ['LoadFontEx', 'DrawTextEx']
+++


This is intended to be a sort of cheat-sheet, mainly for myself, on how to do some of the very basic things with [raylib](https://www.raylib.com/).
The info is almost certainly available elsewhere, but by writing it down in my own style, it helps me remember.

# Main Structure

The main raylib program is pretty typical.
There is some initialization code:

```c#
Raylib.InitWindow(WindowWidth, WindowHeight, "3D Playground");
Raylib.SetTargetFPS(60);
```

And then there is a loop that keeps running until the app is done (the `sceneManager` is my own concoction).

```c#
while (!Raylib.WindowShouldClose() && !sceneManager.ExitRequested)
{
    sceneManager.Update();

    Raylib.BeginDrawing();
    Raylib.ClearBackground(Color.Black);
    sceneManager.Draw();
    Raylib.EndDrawing();
}
```

When the while loop exits, the app should clean up after itself.
For a simple app, this just means closing the window:

```c#
Raylib.CloseWindow();
```


# Using a custom font

Unlike monogame, fonts (and other resources) are loaded at runtime.
No pipeline to deal with.
The steps are basically:

1. Use `LoadFontEx` to load the font
2. Use `DrawTextEx` to draw with it
3. When done with the font, use `UnloadFont` to release it

Loading it fairly simple, but there are two things to be aware of.

First, be sure to check if the font that was just loaded is actually valid.
Second, the codepoints array, as I understand it, specifies the glyphs to load (this example uses ASCII printable characters).

```c#
var path = Path.Combine(AppContext.BaseDirectory, "assets", "fonts", "ancizar-sans.ttf");
var codepoints = Enumerable.Range(32, 95).ToArray();

var myFont = Raylib.LoadFontEx(path, 48, codepoints, codepoints.Length);

if (!Raylib.IsFontValid(myFont))
{
    // TODO - create an AssetLoadException or some such
    throw new InvalidOperationException($"Font in {path} is not valid.");
}
```

Drawing is similar to drawing with the built-in font, but uses a vector for the position, and a float for the spacing between characters (`1f` in my example):

```c#
Raylib.DrawTextEx(myFont, titleText, new Vector2(40, 40), 32f, 1f, Color.RayWhite);
```

The font needs to be accessible at runtime, so you'll want something like the following in your `.csproj` file, to copy assets to the output directory:

```xml
<ItemGroup>
    <None Include="assets/**" CopyToOutputDirectory="PreserveNewest" />
</ItemGroup>
```

I put the font handling code in a [Fonts class](https://github.com/dswisher/3d-playground/blob/d9bd2023f00ee8202f66beb58db8a34c4d2da8be/src/Playground/Fonts.cs).


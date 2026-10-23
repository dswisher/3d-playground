+++
date = '2026-10-23T08:30:05-05:00'
draft = false
title = 'raylib Basics'
slug = 'raylib-basics'
tags = []
+++

# Main Structure

This is intended to be a sort of cheat-sheet, mainly for myself, on how to do some of the basic things with [raylib](https://www.raylib.com/).
The info is almost certainly available elsewhere, but by writing it down in my own style, it helps me remember.

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

# Using a custom font

TBD.


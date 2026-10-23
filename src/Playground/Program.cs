// Copyright (c) Doug Swisher. All Rights Reserved.
// Licensed under the MIT License. See LICENSE.md in the project root for license information.

using Raylib_cs;

namespace Playground
{
    internal static class Program
    {
        private const int WindowWidth = 1280;
        private const int WindowHeight = 720;

        private static void Main()
        {
            Raylib.InitWindow(WindowWidth, WindowHeight, "3D Playground");
            Raylib.SetExitKey(KeyboardKey.Null);
            Raylib.SetTargetFPS(60);

            var sceneManager = new SceneManager();

            try
            {
                sceneManager.Push(new MenuScene("3D Playground", new MenuItem[]
                {
                    new("child scene 1", KeyboardKey.One, () => new PlaceholderScene("Child scene 1")),
                    new("child scene 2", KeyboardKey.Two, () => new PlaceholderScene("Child scene 2")),
                }));

                while (!Raylib.WindowShouldClose() && !sceneManager.ExitRequested)
                {
                    sceneManager.Update();

                    Raylib.BeginDrawing();
                    Raylib.ClearBackground(Color.Black);
                    sceneManager.Draw();
                    Raylib.EndDrawing();
                }
            }
            finally
            {
                sceneManager.Shutdown();
                Raylib.CloseWindow();
            }
        }
    }
}

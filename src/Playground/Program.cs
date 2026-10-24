// Copyright (c) Doug Swisher. All Rights Reserved.
// Licensed under the MIT License. See LICENSE.md in the project root for license information.

using Playground.Scenes;
using Playground.Scenes.Menus;
using Raylib_cs;

namespace Playground
{
    public static class Program
    {
        private const int WindowWidth = 1280;
        private const int WindowHeight = 720;

        public static void Main()
        {
            Raylib.InitWindow(WindowWidth, WindowHeight, "3D Playground");
            Raylib.SetTargetFPS(60);

            // We want to use ESC to exit child scenes, so we disable it as a global exit key
            Raylib.SetExitKey(KeyboardKey.Null);

            var sceneManager = new SceneManager();

            try
            {
                Fonts.Load();

                sceneManager.Push(MenuBuilder.BuildMainMenu());

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
                Fonts.Unload();
                Raylib.CloseWindow();
            }
        }
    }
}

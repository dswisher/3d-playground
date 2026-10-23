// Copyright (c) Doug Swisher. All Rights Reserved.
// Licensed under the MIT License. See LICENSE.md in the project root for license information.

using Raylib_cs;

namespace Playground
{
    internal sealed class PlaceholderScene : IScene
    {
        private readonly string titleText;

        public PlaceholderScene(string title)
        {
            titleText = title;
        }


        public void OnEnter()
        {
        }


        public void OnExit()
        {
        }


        public void Update(SceneManager sceneManager)
        {
            if (Raylib.IsKeyPressed(KeyboardKey.Escape))
            {
                sceneManager.BackOrExit();
            }
        }


        public void Draw()
        {
            Raylib.DrawText(titleText, 40, 40, 32, Color.RayWhite);
            Raylib.DrawText("ESC - back", 40, 100, 24, Color.Gray);
        }
    }
}

// Copyright (c) Doug Swisher. All Rights Reserved.
// Licensed under the MIT License. See LICENSE.md in the project root for license information.

using System.Numerics;
using Raylib_cs;

namespace Playground.Scenes.Menus
{
    public sealed class PlaceholderScene : AbstractScene
    {
        private readonly string titleText;

        public PlaceholderScene(string title)
        {
            titleText = title;
        }


        public override void Update(SceneManager sceneManager)
        {
            if (Raylib.IsKeyPressed(KeyboardKey.Escape))
            {
                sceneManager.BackOrExit();
            }
        }


        public override void Draw()
        {
            Raylib.DrawTextEx(Fonts.Main, titleText, new Vector2(40, 40), 32f, 1f, Color.RayWhite);
            Raylib.DrawTextEx(Fonts.Main, "ESC - back", new Vector2(40, 100), 24f, 1f, Color.RayWhite);
        }
    }
}

// Copyright (c) Doug Swisher. All Rights Reserved.
// Licensed under the MIT License. See LICENSE.md in the project root for license information.

using System.Numerics;
using Raylib_cs;

namespace Playground.Scenes
{
    public class Draw3DPrimitiveScene : IScene
    {
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
            Raylib.DrawTextEx(Fonts.Main, "Drawing a 3D Primitive", new Vector2(40, 40), 32f, 1f, Color.RayWhite);
            Raylib.DrawTextEx(Fonts.Main, "ESC - back", new Vector2(40, 100), 24f, 1f, Color.RayWhite);
        }
    }
}

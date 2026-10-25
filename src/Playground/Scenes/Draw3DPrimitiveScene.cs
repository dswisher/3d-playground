// Copyright (c) Doug Swisher. All Rights Reserved.
// Licensed under the MIT License. See LICENSE.md in the project root for license information.

using System.Numerics;
using Playground.Helpers;
using Raylib_cs;

namespace Playground.Scenes
{
    public class Draw3DPrimitiveScene : AbstractScene
    {
        private readonly FlyingCamera flyingCamera;

        private Camera3D camera;


        public Draw3DPrimitiveScene()
        {
            BackgroundColor = Color.SkyBlue;

            // Define the camera to look into our 3d world
            camera = new()
            {
                Position = new Vector3(6.0f, 6.0f, 6.0f),
                Target = new Vector3(0.0f, 0.0f, 0.0f),
                Up = new Vector3(0.0f, 1.0f, 0.0f),
                FovY = 45.0f,
                Projection = CameraProjection.Perspective
            };

            flyingCamera = new FlyingCamera(camera);
        }


        public override void Update(SceneManager sceneManager)
        {
            if (Raylib.IsKeyPressed(KeyboardKey.Escape))
            {
                sceneManager.BackOrExit();
            }

            flyingCamera.Update(ref camera);
        }


        public override void Draw()
        {
            var height = Raylib.GetScreenHeight();

            Raylib.DrawTextEx(Fonts.Main, "ESC - back", new Vector2(20, height - 50), 24f, 1f, Color.Black);

            flyingCamera.Draw(camera);

            Raylib.DrawFPS(10, 10);

            // Let's draw in 3D!
            Raylib.BeginMode3D(camera);

            Raylib.DrawPlane(new Vector3(0.0f, 0.0f, 0.0f), new Vector2(20.0f, 20.0f), Color.LightGray);

            Raylib.DrawSphere(new Vector3(-3.0f, 1.0f, 0.0f), 1.0f, Color.Blue);

            Raylib.DrawCylinder(
                new Vector3(0.0f, 0.0f, 0.0f),
                1.0f,  // top radius
                1.0f,  // bottom radius
                2.0f,  // height
                16,    // slices
                Color.Green);

            Raylib.DrawCube(new Vector3(3.0f, 1.0f, 0.0f), 2.0f, 2.0f, 2.0f, Color.Red);

            // Done drawing in 3D
            Raylib.EndMode3D();
        }
    }
}

// Copyright (c) Doug Swisher. All Rights Reserved.
// Licensed under the MIT License. See LICENSE.md in the project root for license information.

using System;
using System.IO;
using System.Numerics;
using Playground.Helpers;
using Raylib_cs;

namespace Playground.Scenes
{
    public class HouseModelScene : AbstractScene
    {
        private readonly FlyingCamera flyingCamera;

        private Camera3D camera;
        private Model? houseModel;


        public HouseModelScene()
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


        public override void OnEnter()
        {
            var path = Path.Combine(AppContext.BaseDirectory, "assets", "models", "house-1.glb");

            houseModel = Raylib.LoadModel(path);

            if (houseModel != null && !Raylib.IsModelValid(houseModel.Value))
            {
                // TODO - create an AssetLoadException or some such
                throw new InvalidOperationException($"Model in {path} is not valid.");
            }
        }


        public override void OnExit()
        {
            if (houseModel != null)
            {
                Raylib.UnloadModel(houseModel.Value);

                houseModel = null;
            }
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

            if (houseModel != null)
            {
                Raylib.BeginMode3D(camera);

                var position = Vector3.Zero;

                Raylib.DrawModel(houseModel.Value, position, 1.0f, Color.White);

                Raylib.EndMode3D();
            }
        }
    }
}

// Copyright (c) Doug Swisher. All Rights Reserved.
// Licensed under the MIT License. See LICENSE.md in the project root for license information.

using System.Collections.Generic;
using Raylib_cs;

namespace Playground.Scenes
{
    public sealed class SceneManager
    {
        private readonly Stack<IScene> scenes = new();

        public bool ExitRequested { get; private set; }


        public void Push(IScene scene)
        {
            if (scenes.TryPeek(out var currentScene))
            {
                currentScene.OnExit();
            }

            scenes.Push(scene);
            scene.OnEnter();
        }


        public void BackOrExit()
        {
            if (scenes.Count <= 1)
            {
                ExitRequested = true;
                return;
            }

            if (scenes.TryPop(out var currentScene))
            {
                currentScene.OnExit();
            }

            if (scenes.TryPeek(out var previousScene))
            {
                previousScene.OnEnter();
            }
        }


        public void Update()
        {
            if (scenes.TryPeek(out var currentScene))
            {
                currentScene.Update(this);
            }
        }


        public void Draw()
        {
            if (scenes.TryPeek(out var currentScene))
            {
                Raylib.BeginDrawing();
                Raylib.ClearBackground(currentScene.BackgroundColor);

                currentScene.Draw();

                Raylib.EndDrawing();
            }
        }


        public void Shutdown()
        {
            if (scenes.TryPop(out var currentScene))
            {
                currentScene.OnExit();
            }

            scenes.Clear();
        }
    }
}

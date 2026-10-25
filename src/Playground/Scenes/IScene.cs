// Copyright (c) Doug Swisher. All Rights Reserved.
// Licensed under the MIT License. See LICENSE.md in the project root for license information.

using Raylib_cs;

namespace Playground.Scenes
{
    public interface IScene
    {
        Color BackgroundColor { get; }

        void OnEnter();

        void OnExit();

        void Update(SceneManager sceneManager);

        void Draw();
    }
}

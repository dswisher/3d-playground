// Copyright (c) Doug Swisher. All Rights Reserved.
// Licensed under the MIT License. See LICENSE.md in the project root for license information.

using Raylib_cs;

namespace Playground.Scenes
{
    public abstract class AbstractScene : IScene
    {
        public Color BackgroundColor { get; set; } = Color.Black;

        public virtual void OnEnter()
        {
        }


        public virtual void OnExit()
        {
        }


        public abstract void Update(SceneManager sceneManager);


        public abstract void Draw();
    }
}

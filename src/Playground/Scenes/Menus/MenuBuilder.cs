// Copyright (c) Doug Swisher. All Rights Reserved.
// Licensed under the MIT License. See LICENSE.md in the project root for license information.

using Raylib_cs;

namespace Playground.Scenes.Menus
{
    public static class MenuBuilder
    {
        public static MenuScene BuildMainMenu()
        {
            return new MenuScene("3D Playground", new MenuItem[]
            {
                new("Drawing 3D Objects", KeyboardKey.One, BuildDrawMenu),
                new("child scene 2", KeyboardKey.Two, () => new PlaceholderScene("Child scene 2")),
            });
        }


        public static MenuScene BuildDrawMenu()
        {
            return new MenuScene("Drawing 3D Objects", new MenuItem[]
            {
                new("Drawing a 3D Primitive", KeyboardKey.One, () => new Draw3DPrimitiveScene())
            });
        }
    }
}

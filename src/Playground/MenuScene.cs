// Copyright (c) Doug Swisher. All Rights Reserved.
// Licensed under the MIT License. See LICENSE.md in the project root for license information.

using System.Collections.Generic;
using System.Numerics;
using Raylib_cs;

namespace Playground
{
    internal sealed class MenuScene : IScene
    {
        private const int LeftMargin = 40;
        private const int FirstItemY = 120;
        private const int ItemLineHeight = 36;

        private readonly string titleText;
        private readonly IReadOnlyList<MenuItem> menuItems;
        private readonly string escapeLabel;


        public MenuScene(string title, IReadOnlyList<MenuItem> items, string escapeLabel = "exit")
        {
            titleText = title;
            menuItems = items;
            this.escapeLabel = escapeLabel;
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
                return;
            }

            foreach (var item in menuItems)
            {
                if (Raylib.IsKeyPressed(item.Hotkey))
                {
                    sceneManager.Push(item.CreateScene());
                    return;
                }
            }
        }


        public void Draw()
        {
            Raylib.DrawTextEx(Fonts.Main, titleText, new Vector2(LeftMargin, 40), 32f, 1f, Color.RayWhite);

            for (var index = 0; index < menuItems.Count; index++)
            {
                var item = menuItems[index];
                var line = $"{FormatHotkey(item.Hotkey)} - {item.Label}";
                Raylib.DrawTextEx(Fonts.Main, line, new Vector2(LeftMargin, FirstItemY + index * ItemLineHeight), 24f, 1f, Color.RayWhite);
            }

            Raylib.DrawTextEx(Fonts.Main, $"ESC - {escapeLabel}", new Vector2(LeftMargin, FirstItemY + (menuItems.Count + 1) * ItemLineHeight), 24f, 1f, Color.Gray);
        }


        private static string FormatHotkey(KeyboardKey hotkey)
        {
            return hotkey switch
            {
                KeyboardKey.Zero => "0",
                KeyboardKey.One => "1",
                KeyboardKey.Two => "2",
                KeyboardKey.Three => "3",
                KeyboardKey.Four => "4",
                KeyboardKey.Five => "5",
                KeyboardKey.Six => "6",
                KeyboardKey.Seven => "7",
                KeyboardKey.Eight => "8",
                KeyboardKey.Nine => "9",
                _ => hotkey.ToString(),
            };
        }
    }
}

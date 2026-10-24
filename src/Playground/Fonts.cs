// Copyright (c) Doug Swisher. All Rights Reserved.
// Licensed under the MIT License. See LICENSE.md in the project root for license information.

using System;
using System.IO;
using System.Linq;
using Raylib_cs;

namespace Playground
{
    public static class Fonts
    {
        private static Font? main;


        public static Font Main
        {
            get
            {
                if (main == null)
                {
                    throw new InvalidOperationException("Main font not yet loaded.");
                }

                return main.Value;
            }
        }


        public static void Load()
        {
            var path = Path.Combine(AppContext.BaseDirectory, "assets", "fonts", "airborne2.ttf");
            var codepoints = Enumerable.Range(32, 95).ToArray();

            var loaded = Raylib.LoadFontEx(path, 48, codepoints, codepoints.Length);

            if (!Raylib.IsFontValid(loaded))
            {
                // TODO - create an AssetLoadException or some such
                throw new InvalidOperationException($"Font in {path} is not valid.");
            }

            main = loaded;
        }


        public static void Unload()
        {
            if (main != null)
            {
                Raylib.UnloadFont(main.Value);
                main = null;
            }
        }
    }
}

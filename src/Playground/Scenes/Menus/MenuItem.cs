// Copyright (c) Doug Swisher. All Rights Reserved.
// Licensed under the MIT License. See LICENSE.md in the project root for license information.

using System;
using Raylib_cs;

namespace Playground.Scenes.Menus
{
    public sealed record MenuItem(string Label, KeyboardKey Hotkey, Func<IScene> CreateScene);
}

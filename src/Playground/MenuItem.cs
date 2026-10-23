// Copyright (c) Doug Swisher. All Rights Reserved.
// Licensed under the MIT License. See LICENSE.md in the project root for license information.

using System;

using Raylib_cs;

namespace Playground
{
    internal sealed record MenuItem(string Label, KeyboardKey Hotkey, Func<IScene> CreateScene);
}

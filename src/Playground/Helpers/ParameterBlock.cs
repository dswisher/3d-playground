// Copyright (c) Doug Swisher. All Rights Reserved.
// Licensed under the MIT License. See LICENSE.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Raylib_cs;

namespace Playground.Helpers
{
    public class ParameterBlock
    {
        private const float Spacing = 1f;

        private readonly List<Line> lines = new();
        private readonly float fontSize;
        private readonly Color color;

        public ParameterBlock(float fontSize, Color color)
        {
            this.fontSize = fontSize;
            this.color = color;
        }


        public enum AnchorPoint
        {
            TopLeft,
            TopRight
        }


        public void Add(string name, string value)
        {
            name = $"{name}: ";

            var nameSize = Raylib.MeasureTextEx(Fonts.Main, name, fontSize, Spacing);
            var valueSize = Raylib.MeasureTextEx(Fonts.Main, value, fontSize, Spacing);

            lines.Add(new Line(name, value, nameSize, valueSize));
        }


        public void Draw(float x, float y, AnchorPoint anchor = AnchorPoint.TopLeft)
        {
            // If there are no lines, there is nothing to do
            if (lines.Count == 0)
            {
                return;
            }

            // Find the length of the longest name and longest value
            var maxNameLen = lines.Select(l => l.NameSize.X).Max();
            var maxValueLen = lines.Select(l => l.ValueSize.X).Max();

            // Find the upper-left corner
            var point = anchor switch
            {
                AnchorPoint.TopLeft => new Vector2(x, y),
                AnchorPoint.TopRight => new Vector2(x - maxNameLen - maxValueLen, y),
                _ => throw new NotImplementedException($"Usage of anchor {anchor} is not implemented.")
            };

            // Draw all the lines
            foreach (var line in lines)
            {
                Raylib.DrawTextEx(Fonts.Main, line.Name, point with { X = point.X + maxNameLen - line.NameSize.X }, fontSize, Spacing, color);
                Raylib.DrawTextEx(Fonts.Main, line.Value, point with { X = point.X + maxNameLen }, fontSize, Spacing, color);

                point.Y += float.Max(line.NameSize.Y, line.ValueSize.Y);
            }
        }


        private sealed record Line(string Name, string Value, Vector2 NameSize, Vector2 ValueSize);
    }
}

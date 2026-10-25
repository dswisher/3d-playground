// Copyright (c) Doug Swisher. All Rights Reserved.
// Licensed under the MIT License. See LICENSE.md in the project root for license information.

using System;
using System.Numerics;
using Raylib_cs;

namespace Playground.Helpers
{
    public class FlyingCamera
    {
        private const float LookSpeed = 0.25f;
        private const float MoveSpeed = 3.0f;
        private const float FovSpeed = 3.0f;
        private const float PitchLimit = 89f * MathF.PI / 180f;

        private readonly Camera3D originalCamera;

        private float yaw;
        private float pitch;
        private float roll;

        public FlyingCamera(Camera3D camera)
        {
            originalCamera = camera;

            CalcYawPitchRoll(camera);
        }


        /// <summary>
        /// Elevation above the XZ plane, in degrees.
        /// </summary>
        private float Pitch => pitch * 180f / MathF.PI;

        /// <summary>
        /// Heading around world Y, measured from +Z in degrees.
        /// </summary>
        private float Yaw => yaw * 180f / MathF.PI;

        /// <summary>
        /// Signed angle from the unrolled up direction to camera.Up, around forward, in degrees.
        /// </summary>
        private float Roll => roll * 180f / MathF.PI;


        public void Update(ref Camera3D camera)
        {
            if (Raylib.IsKeyPressed(KeyboardKey.R))
            {
                camera = originalCamera;

                CalcYawPitchRoll(camera);

                return;
            }

            if (Raylib.IsKeyPressed(KeyboardKey.P))
            {
                camera.Projection = camera.Projection == CameraProjection.Perspective ? CameraProjection.Orthographic : CameraProjection.Perspective;
            }

            if (Raylib.IsKeyDown(KeyboardKey.Equal) || Raylib.IsKeyDown(KeyboardKey.KpAdd))
            {
                camera.FovY = Math.Clamp(camera.FovY + FovSpeed * Raylib.GetFrameTime(), 1f, 89f);
            }

            if (Raylib.IsKeyDown(KeyboardKey.Minus) || Raylib.IsKeyDown(KeyboardKey.KpSubtract))
            {
                camera.FovY = Math.Clamp(camera.FovY - FovSpeed * Raylib.GetFrameTime(), 1f, 89f);
            }

            var yawInput = 0f;
            var pitchInput = 0f;
            var rollInput = 0f;

            if (Raylib.IsKeyDown(KeyboardKey.Right))
            {
                yawInput = -1f;
            }

            if (Raylib.IsKeyDown(KeyboardKey.Left))
            {
                yawInput = 1f;
            }

            if (Raylib.IsKeyDown(KeyboardKey.Up))
            {
                pitchInput = 1f;
            }

            if (Raylib.IsKeyDown(KeyboardKey.Down))
            {
                pitchInput = -1f;
            }

            if (Raylib.IsKeyDown(KeyboardKey.Z))
            {
                rollInput = -1f;
            }

            if (Raylib.IsKeyDown(KeyboardKey.C))
            {
                rollInput = 1f;
            }

            roll += rollInput * LookSpeed * Raylib.GetFrameTime();

            // Keep pitch from being straight up or down, where the math gets ambiguous
            pitch = Math.Clamp(
                pitch + pitchInput * LookSpeed * Raylib.GetFrameTime(),
                -PitchLimit,
                PitchLimit);

            yaw += yawInput * LookSpeed * Raylib.GetFrameTime();

            var cosPitch = MathF.Cos(pitch);
            var forward = new Vector3(
                MathF.Sin(yaw) * cosPitch,
                MathF.Sin(pitch),
                MathF.Cos(yaw) * cosPitch);

            var worldUp = Vector3.UnitY;
            var uprightRight = Vector3.Normalize(Vector3.Cross(forward, worldUp));
            var uprightUp = Vector3.Normalize(Vector3.Cross(uprightRight, forward));

            var rollRotation = Quaternion.CreateFromAxisAngle(forward, roll);
            camera.Up = Vector3.Normalize(Vector3.Transform(uprightUp, rollRotation));
            camera.Target = camera.Position + forward;

            var right = Vector3.Normalize(Vector3.Cross(forward, camera.Up));
            var up = Vector3.Normalize(Vector3.Cross(right, forward));

            var direction = Vector3.Zero;

            if (Raylib.IsKeyDown(KeyboardKey.A))
            {
                direction -= right;
            }

            if (Raylib.IsKeyDown(KeyboardKey.D))
            {
                direction += right;
            }

            if (Raylib.IsKeyDown(KeyboardKey.W))
            {
                direction += forward;
            }

            if (Raylib.IsKeyDown(KeyboardKey.S))
            {
                direction -= forward;
            }

            if (Raylib.IsKeyDown(KeyboardKey.Q))
            {
                direction -= up;
            }

            if (Raylib.IsKeyDown(KeyboardKey.E))
            {
                direction += up;
            }

            if (direction.LengthSquared() > 0)
            {
                var movement = Vector3.Normalize(direction) * MoveSpeed * Raylib.GetFrameTime();

                camera.Position += movement;
                camera.Target += movement;
            }
        }


        public void Draw(Camera3D camera)
        {
            var width = Raylib.GetScreenWidth();

            var block = new ParameterBlock(20f, Color.Black);

            block.Add("Position", $"({camera.Position.X:N2}, {camera.Position.Y:N2}, {camera.Position.Z:N2})");
            block.Add("Target", $"({camera.Target.X:N2}, {camera.Target.Y:N2}, {camera.Target.Z:N2})");
            block.Add("Up", $"({camera.Up.X:N2}, {camera.Up.Y:N2}, {camera.Up.Z:N2})");
            block.Add("FovY", $"{camera.FovY:N2}");
            block.Add("Projection", $"{camera.Projection}");
            block.Add("Yaw/Pitch/Roll", $"{Yaw:N0} / {Pitch:N0} / {Roll:N0}");

            block.Draw(width - 30, 10, ParameterBlock.AnchorPoint.TopRight);
        }


        private void CalcYawPitchRoll(Camera3D camera)
        {
            var initialForward = Vector3.Normalize(camera.Target - camera.Position);

            // With our convention, forward.X = cos(pitch) * sin(yaw) and
            // forward.Z = cos(pitch) * cos(yaw). atan2(X, Z) recovers the
            // heading from +Z and handles all quadrants.
            yaw = MathF.Atan2(initialForward.X, initialForward.Z);

            // Because forward is a unit vector, forward.Y = sin(pitch).
            // asin(Y) therefore recovers the elevation angle.
            pitch = MathF.Asin(Math.Clamp(initialForward.Y, -1f, 1f));

            // dot(uprightUp, cameraUp) is cos(roll); the cross product's
            // component along forward is signed sin(roll). atan2 recovers
            // the signed angle around the forward axis.
            var worldUp = Vector3.UnitY;
            var uprightRight = Vector3.Cross(initialForward, worldUp);

            // Handle a view direction nearly parallel to world-up.
            if (uprightRight.LengthSquared() < 1e-6f)
            {
                uprightRight = Vector3.Cross(initialForward, Vector3.UnitZ);
            }

            uprightRight = Vector3.Normalize(uprightRight);
            var uprightUp = Vector3.Normalize(Vector3.Cross(uprightRight, initialForward));

            // Project camera.Up onto the plane perpendicular to forward.
            var cameraUp = camera.Up - Vector3.Dot(camera.Up, initialForward) * initialForward;

            if (cameraUp.LengthSquared() < 1e-6f)
            {
                roll = 0f; // camera.Up is invalid/parallel to forward
            }
            else
            {
                cameraUp = Vector3.Normalize(cameraUp);

                roll = MathF.Atan2(
                    Vector3.Dot(initialForward, Vector3.Cross(uprightUp, cameraUp)),
                    Vector3.Dot(uprightUp, cameraUp));
            }
        }
    }
}

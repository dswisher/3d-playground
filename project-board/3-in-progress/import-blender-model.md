---
type: story
color: blue
priority: medium
depends_on:
  - simple-blender-model
---

# Import Blender Model

Import a Blender model into raylib and visualize it.


## Acceptance Criteria

- [ ] The `.glb` file imports without error
- [ ] The model is visible in a new scene, with expected orientation and a useful scale/framing.
- [ ] The model is unloaded when leaving the scene
- [ ] The devlog entry has been updated with the loading steps and any noted differences between the rendering in Blender and raylib


## Sketch

Add a scene such as `HouseModelScene` that follows the existing `Draw3DPrimitiveScene` pattern:

- In `OnEnter()`, load `assets/models/house-1.glb` with `Raylib.LoadModel(...)` and check it with `Raylib.IsModelValid(...)`.
- In `Draw()`, call `Raylib.BeginMode3D(camera)`, then `Raylib.DrawModel(model, position, scale, Color.White)`, then `Raylib.EndMode3D()`.
- In `OnExit()`, call `Raylib.UnloadModel(model)`.
- In `Update()`, handle Escape with `sceneManager.BackOrExit()` and update a `FlyingCamera`.
- Register the new scene in `MenuBuilder` so it can be opened from the app menu.

For a first pass, try drawing at `Vector3.Zero` with scale `1.0f` and aim the camera at the origin. Adjust the camera distance or scale if the
house is too large or small; the model’s origin and dimensions affect framing. You can keep the existing ground plane as a visual reference.


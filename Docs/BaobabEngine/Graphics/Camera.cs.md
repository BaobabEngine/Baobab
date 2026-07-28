# [Camera](../../../BaobabEngine/Graphics/Camera.cs)
A class that can be used to modify the position of all images drawn to the screen.

## Constructor
- `public Camera(Vector2 origin, Vector2 startingPosition, bool relative = false)`

## Properties
- `public Vector2 Origin` - Stores the origin at which the camera is drawn. The most common origin points would be (0, 0) or the camera's center.
- `public Vector2 Position` - Stores the position for where the world is drawn.
- `public Vector2 Zoom` - Stores the zoom (or otherwise scale) at world is drawn at.
- `public float Rotation` - Stores the rotation at which the world is drawn at.
- `public Matrix3x2 Matrix` - Returns a matrix representing the camera's current state.

## Methods
- `public void Apply(in Batcher batcher)` - Pushes the camera's matrix to the batcher.
- `public void Reset()` - Reset's the camera's state to the same state when it was originally created.

The camera type can be implicitely converted to a `Matrix3x2`.
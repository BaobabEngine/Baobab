# [Bounding Box](../../../BaobabEngine/Collisions/BoundingBox.cs)
The bounding box is a collision shape that can be used to detect if two objects are colliding. It uses simple AABB collisions checking.

## Constructors
- `public BoundingBox(Sprite sprite, Vector2 position)`
- `public BoundingBox(Vector2 position, float width, float height)`

## Properties
- `public float Top` - Returns the Y position of the top of the bounding box.
- `public float Bottom` - Returns the Y position of the bottom of the bounding box.
- `public float Left` - Returns the X position of the left of the bounding box.
- `public float Right` - Returns the X position of the right of the bounding box.

## Methods
- `public void ScaleBoundingBox(float scale)` - Scales the width and height of the bounding box using the given scale. This can be useful if you don't want you bounding box to wrap to the full size of a sprite.
- `public bool Intersects(BoundingBox other)` - Returns true if one bounding box is colliding with another.
- `public bool Intersects(CircleBound other)` - Returns true if the bounding box is colliding with a [circle bound](../../../BaobabEngine/Collisions/CircleBound.cs).
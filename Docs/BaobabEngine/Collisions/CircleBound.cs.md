# [CircleBound](../../../BaobabEngine/Collisions/CircleBound.cs)
A circular collision boundary used for checking of two circles are colliding.

## Constructors
- `public CircleBound(Vector2 circleCenter, Sprite sprite)`
- `public CircleBound(Vector circleCenter, float radiusLength)`

## Properties
- `public Vector2 Center` - The center of the circle.
- `public float Top` - Returns the Y coordinate for the top of the circle.
- `public float Bottom` - Returns the Y coordinate for the bottom of the circle.
- `public float Left` - Returns the X coordinate for the left of the circle.
- `public float Right` - Returns the X coordinate for the right of the circle.
- `public float Radius` - The radius of the circle.

## Methods
- `public void ScaleBounds(float scale)` - Scales the circle boundary. This can be useful to ensure a circle's boundary does not overlap their sprite.
- `public bool Intersects(CircleBound other)` - Returns true if the two circles are intersecting each other.
- `public bool Intersects(BoundingBox other)` - Returns true if the circle bound and [bounding box](../../../BaobabEngine/Collisions/BoundingBox.cs) are colliding with each other.
using BaobabEngine.Graphics;
using System.Numerics;

namespace BaobabEngine.Collisions;

/// <summary>
/// A type representing the circular bounds of an object.
/// Used for collisions.
/// </summary>
public readonly struct CircleBound
{
    public readonly Vector2 Center;

    public float Top => Center.Y - Radius;
    public float Bottom => Center.Y + Radius;

    public float Left => Center.X - Radius;
    public float Right => Center.X + Radius;
    
    public readonly float Radius;

    public CircleBound(Vector2 circleCenter, Sprite sprite, float scale = 1f)
    {
        Center = circleCenter;
        Radius = (sprite.Width / 2) * scale;
    }
    public CircleBound(Vector2 circleCenter, float radiusLength)
    {
        Center = circleCenter;
        Radius = radiusLength;
    }

    public bool Intersects(CircleBound other)
    {
        return Vector2.DistanceSquared(Center, other.Center) < Radius * other.Radius;
    }

    public bool Intersects(BoundingBox other)
    {
        return other.Intersects(this);
    }
}

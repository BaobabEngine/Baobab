using BaobabEngine.Graphics;
using System.Numerics;

namespace BaobabEngine.Collisions;

public class CircleBound
{
    public Vector2 Center;

    public float Top => Center.Y - Radius;
    public float Bottom => Center.Y + Radius;

    public float Left => Center.X - Radius;
    public float Right => Center.X + Radius;
    
    public float Radius;

    public CircleBound(Vector2 circleCenter, Sprite sprite)
    {
        Center = circleCenter;
        Radius = sprite.Width / 2;
    }
    public CircleBound(Vector2 circleCenter, float radiusLength)
    {
        Center = circleCenter;
        Radius = radiusLength;
    }

    public void ScaleBounds(float scale)
    {
        Radius *= scale;
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
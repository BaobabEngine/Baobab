using System.Numerics;
using Foster.Framework;

namespace BaobabEngine.Graphics;

public class Camera(Vector2 origin, Vector2 startingPosition, bool relative = false)
{
    private readonly Vector2 _startingPosition = startingPosition;
    
    public Vector2 Origin = origin;
    public Vector2 Position = startingPosition;
    public float Zoom = 1.0f;
    public float Rotation;

    public bool Relative = relative;

    public Matrix3x2 Matrix => Transform.CreateMatrix(Position, Origin, new Vector2(Zoom), Rotation);

    // Pushes the camera to the batcher to be applied when rendering
    public void Apply(in Batcher batcher)
    {
        batcher.PushMatrix(Matrix, Relative);
    }

    public void Reset()
    {
        Position = _startingPosition;
        Zoom = 1.0f;
        Rotation = 0.0f;
    }

    public static implicit operator Matrix3x2(Camera cam)
    {
        return cam.Matrix;
    }
}

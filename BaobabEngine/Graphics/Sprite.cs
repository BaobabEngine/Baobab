using Foster.Framework;
using System.Numerics;

namespace BaobabEngine.Graphics;

public class Sprite
{
    public Subtexture Texture;
    
    public float Scale;

    public float Rotation;
    
    public float Width => Texture.Width * Scale;
    public float Height => Texture.Height * Scale;

    public enum DrawPosition
    {
        Centered,
        TopLeft
    }
    
    public Sprite() { }

    public Sprite(Subtexture spriteTexture, float spriteScale, float spriteRotation = 0.0f)
    {
        Texture = spriteTexture;
        Scale = spriteScale;
        Rotation = spriteRotation;
    }

    public void Draw(in Batcher batcher, Vector2 position, Vector2 origin, bool mirrorX = false, bool mirrorY = false)
    {
        // Get the mirror scale using the arguments
        var xScale = (mirrorX) ? -Scale : Scale;
        var yScale = (mirrorY) ? -Scale : Scale;

        batcher.Image(Texture, position, origin, 
            new Vector2(xScale, yScale), Rotation, Color.White);
    }

    public void Draw(in Batcher batcher, Vector2 position, DrawPosition origin = DrawPosition.Centered, 
                     bool mirrorX = false, bool mirrorY = false)
    {
        Vector2 originVector = new();
        switch (origin)
        {
            case DrawPosition.Centered:
                originVector = new Vector2(Width * .5f, Height * .5f);
                break;
            case DrawPosition.TopLeft:
                originVector = new Vector2(0f);
                break;
        }

        Draw(batcher, position, originVector, mirrorX, mirrorY);
    }
}


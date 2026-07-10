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
    
    public Sprite() { }

    public Sprite(Subtexture spriteTexture, float spriteScale, float spriteRotation = 0.0f)
    {
        Texture = spriteTexture;
        Scale = spriteScale;
        Rotation = spriteRotation;
    }

    public void Draw(Batcher batcher, Vector2 position)
    {
        batcher.Image(Texture, position, new Vector2(Texture.Width / 2.0f, Texture.Height / 2.0f), 
            new Vector2(Scale), Rotation, Color.White);
    }
    
    // This override allows for mirroring sprites
    public void Draw(Batcher batcher, Vector2 position, bool mirrorX, bool mirrorY)
    {
        // Get the mirror scale using the arguments
        var xScale = (mirrorX) ? -Scale : Scale;
        var yScale = (mirrorY) ? -Scale : Scale;
        
        batcher.Image(Texture, position, new Vector2(Texture.Width / 2, Texture.Height / 2), 
            new Vector2(xScale, yScale), Rotation, Color.White);
    }
}


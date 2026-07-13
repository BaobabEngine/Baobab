using BaobabEngine.Graphics;
using Foster.Framework;
using System.Numerics;

namespace BaobabEngine.UI;

public class Icon : Sprite, IUiElement
{
    public Vector2 Position { get; set; }

    public bool IsVisible { get; set; }

    public Icon(Subtexture texture, Vector2 startingPosition = new()
        , float scale = 1f, float rotation = 0f, 
        bool startsVisible = false)
    {
        Texture = texture;
        Position = startingPosition;
        Scale = scale;
        Rotation = rotation;
        IsVisible = startsVisible;
    }

    public new void Draw(in Batcher batcher, Vector2 position)
    {
        if (!IsVisible) return;
        
        batcher.Image(Texture, position, new Vector2(Texture.Width / 2.0f, Texture.Height / 2.0f), 
            new Vector2(Scale), Rotation, Color.White);
    }

    public new void Draw(in Batcher batcher, Vector2 position, bool mirrorX, bool mirrorY)
    {
        if (!IsVisible) return;
        
        // Get the mirror scale using the arguments
        var xScale = (mirrorX) ? -Scale : Scale;
        var yScale = (mirrorY) ? -Scale : Scale;
        
        batcher.Image(Texture, position, new Vector2(Texture.Width / 2, Texture.Height / 2), 
            new Vector2(xScale, yScale), Rotation, Color.White);
    }

    public void Draw(in Batcher batcher)
    {
        Draw(batcher, Position);
    }

    public void Draw(in Batcher batcher, bool mirrorX, bool mirrorY)
    {
        Draw(batcher, Position, mirrorX, mirrorY);
    }
}
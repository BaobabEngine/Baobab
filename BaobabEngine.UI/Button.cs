using Foster.Framework;
using System;
using System.Numerics;
using BaobabEngine.Collisions;

namespace BaobabEngine.UI;

public class Button(Vector2 position, ThreeSlice texture, TextSet text, bool isVisible = false) : IUiElement
{
    public event EventHandler? IsClicked;

    public Vector2 Position
    {
        get;
        set
        {
            field = value;
            _texture.Position = value;
        }
    } = position;

    public float Width
    {
        get => _texture.Width;
        set => _texture.Width = value;
    }

    public float Height => _texture.Height;

    public bool IsVisible
    {
        get;
        set
        {
            field = value;
            _texture.IsVisible = value;
        }
    } = isVisible;

    public string Text { get; set; } = text.Text;

    public Color FontColor { get; set; } = text.FontColor;

    private ThreeSlice _texture = texture;

    private readonly SpriteFont _font = text.Font;

    private BoundingBox GetBounds()
    {
        var centeredX = Position.X + (Width * .5f);
        var centeredY = Position.Y + (Height * .5f);
        var centeredVector = new Vector2(centeredX, centeredY);

        return new BoundingBox(centeredVector, Width, Height);
    }
    
    public void Draw(in Batcher batcher)
    {
        if (!IsVisible) return;

        var boxCenteredHeight = Position.Y + (Height * .5f);

        var textHeight = _font.HeightOf(Text);
        // TODO: Implement a system for custom width offsets
        var widthOffset = 3;
        var heightOffset = textHeight * .5f;

        Vector2 drawingPosition = new(Position.X + widthOffset, boxCenteredHeight - heightOffset);

        _texture.Draw(batcher);
        _font.Draw(batcher, Text, drawingPosition, FontColor);
    }

    public void Update(Input input, Vector2 offset = new())
    {
        var mousePosition = new Vector2(
            input.Mouse.X + offset.X, 
            input.Mouse.Y + offset.Y
            );
        
        var mouseBounds = new BoundingBox(mousePosition, 1, 1);

        var mouseOverlapsButton = mouseBounds.Intersects(GetBounds());
        var mousePressed = input.Mouse.RightPressed;
        
        if (mouseOverlapsButton && mousePressed) 
            IsClicked?.Invoke(this, EventArgs.Empty);
    }
}

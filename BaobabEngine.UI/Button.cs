using Foster.Framework;
using System.Numerics;
using BaobabEngine.Collisions;

namespace BaobabEngine.UI;

public class Button(Vector2 position, ThreeSlice texture, TextSet text, Vector2 textOffset = new(), bool isVisible = false) : IUiElement
{
    public Vector2 Position
    {
        get;
        set
        {
            field = value;
            _texture.Position = value;
        }
    } = position;

    public Vector2 TextOffset = textOffset;

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

    public bool Pressed => _pressed;
    public bool WasJustPressed => !_pressedLastFrame && _pressed;
    
    private ThreeSlice _texture = texture;
    
    private readonly SpriteFont _font = text.Font;

    private float ScaledWidth => Width * _texture.Scale;
    private float ScaledHeight => Height * _texture.Scale;
    
    private bool _pressed;
    private bool _pressedLastFrame;

    private BoundingBox GetBounds()
    {
        var centeredX = Position.X + (ScaledWidth * .5f);
        var centeredY = Position.Y + (ScaledHeight * .5f);
        var centeredVector = new Vector2(centeredX, centeredY);

        return new BoundingBox(centeredVector, ScaledWidth, ScaledHeight);
    }
    
    public void Draw(in Batcher batcher)
    {
        if (!IsVisible) return;

        var boxCenteredHeight = Position.Y + (Height * .5f);

        var textHeight = _font.HeightOf(Text);
        var heightOffset = TextOffset.Y - (textHeight * .5f) ;

        Vector2 drawingPosition = new(Position.X + TextOffset.X, boxCenteredHeight + heightOffset);

        _texture.Draw(batcher);
        _font.Draw(batcher, Text, drawingPosition, FontColor);
    }
    
    private bool MouseHovering(Input input, Vector2 offset = new())
    {
        var mousePosition = new Vector2(
            input.Mouse.X + offset.X, 
            input.Mouse.Y + offset.Y
        );
        
        var mouseBounds = new BoundingBox(mousePosition, 1, 1);
        return mouseBounds.Intersects(GetBounds());
    }
    
    public void Update(Input input, Vector2 offset = new())
    {
        _pressedLastFrame = _pressed;
        _pressed = MouseHovering(input, offset) && input.Mouse.LeftDown;
    }
}

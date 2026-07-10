using Foster.Framework;

namespace BaobabEngine.UI;

/// <summary>
/// A small collection of data representing text
/// </summary>
public readonly struct TextSet(SpriteFont font, string text, Color color)
{
    public readonly SpriteFont Font = font;
    public readonly string Text = text;
    public readonly Color FontColor = color;
}
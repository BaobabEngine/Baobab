using Foster.Framework;
using System.Numerics;

namespace BaobabEngine.UI;

public interface IUiElement
{
    public Vector2 Position { get; set; }
    
    public bool IsVisible { get; set; }

    public void Draw(in Batcher batcher);
}
using System.Collections.Generic;
using Foster.Framework;

namespace BaobabEngine.Graphics;

public struct Animation(Subtexture[] animationFrames, float delayBetweenFrames)
{
    public Subtexture[] Frames { get; private set; } = animationFrames;
    
    // In seconds
    public float Delay { get; private set; } = delayBetweenFrames;
}
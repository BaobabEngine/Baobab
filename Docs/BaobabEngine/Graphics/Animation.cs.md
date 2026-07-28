# [Animation](../../../BaobabEngine/Graphics/Animation.cs)
A struct containing data describing animations.

## Constructor
- `public Animation(Subtexture[] animationFrames, float delayBetweenFrames)`

## Properties
- `public Subtexture[] Frames { get; private set; }` - A collection of the frames in the animation.
- `public float Delay { get; private set; }` - The delay in seconds between frames.
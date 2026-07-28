# [Animated Sprite](../../../BaobabEngine/Graphics/AnimatedSprite.cs)
A sprite that contains animation data. This class inherits from the [`Sprite`](../../../BaobabEngine/Graphics/Sprite.cs) class.

## Contructor
- `public AnimatedSprite(Dictionary<string, Animation> spriteAnimations, string startingAnimation, float spriteScale = 1, float spriteRotation = 0)`

## Properties
- `private readonly Dictionary<string, Animation> _animations` - Stores the animation that the sprite can play by name.

## Methods
- `public void PlayAnimation(string animationName, bool restartIfNotChanged = true)` - Plays a new animation, taking the animation's name as an argument. Another argument allows you to specify if you would like the animation to restart in the event you are trying to play the same animation. This can help prevent some graphical bugs.
- `public void Update(float deltaTime)` - Updates the sprite and its animation.

*There are more methods and properties, however, they are inherited from the [Sprite](../../../BaobabEngine/Graphics/Sprite.cs) class.*
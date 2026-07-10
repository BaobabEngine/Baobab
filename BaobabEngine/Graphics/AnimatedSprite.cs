using System.Collections.Generic;

namespace BaobabEngine.Graphics;

public class AnimatedSprite: Sprite
{
    // Contains the different animations for a sprite organized by name
    private readonly Dictionary<string, Animation> _animations;
    
    // The current animation that is being played from the dictionary ↑
    private string _currentAnimation;
    private int _currentFrame;

    // The delay between animation frames in seconds
    private float _animationDelay;
    
    // The amount of time that has passed between frames in seconds
    private float _elapsedTime;

    /// <summary>
    /// The main constructor for animated sprites
    /// </summary>
    /// <param name="spriteAnimations">The dictionary containing the different animations for your sprite</param>
    /// <param name="startingAnimation">The key to the animation your sprite will start using from the dictionary</param>
    /// <param name="spriteScale">The scale at which your sprite will be drawn</param>
    /// <param name="spriteRotation">The initial rotation your sprite will have.</param>
    public AnimatedSprite(Dictionary<string, Animation> spriteAnimations,
        string startingAnimation,
        float spriteScale = 1,
        float spriteRotation = 0)
    {
        _animations = spriteAnimations;
        _currentAnimation = startingAnimation;
        _animationDelay = spriteAnimations[startingAnimation].Delay;
        _elapsedTime = 0;
        
        Scale = spriteScale;
        Rotation = spriteRotation;
    }

    public void PlayAnimation(string animationName, bool restartIfNotChanged = true)
    {
        // Checks if an update needs to be made
        if (_currentAnimation == animationName && !restartIfNotChanged)
            return;
            
        // Update animation data
        _currentAnimation = animationName;
        _animationDelay = _animations[_currentAnimation].Delay;
        
        // Resets the frame so it will start at the first frame.
        _currentFrame = 0;
        UpdateTexture();
    }

    // Set's the texture to the current frame in the current animation
    private void UpdateTexture()
    {
        Texture = _animations[_currentAnimation].Frames[_currentFrame];
    }

    public void Update(float deltaTime)
    {
        _elapsedTime += deltaTime;
        if (!(_elapsedTime >= _animationDelay)) return;
            
        // Updates the frame and time values
        _elapsedTime -= _animationDelay;
        _currentFrame = (_currentFrame + 1) % _animations[_currentAnimation].Frames.Length;

        UpdateTexture();
    }
}
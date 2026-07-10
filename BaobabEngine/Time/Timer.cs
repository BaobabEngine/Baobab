using System;

namespace BaobabEngine.Time;

public class Timer(float durationInSeconds)
{
    // Subscribe functions to be run once complete
    public event EventHandler OnComplete;
    
    private float _elapsedTime;
    
    public void Update(float deltaTime, object? sender, EventArgs args)
    {
        _elapsedTime += deltaTime;

        // Invokes subscribed functions and resets the timer once complete
        if (_elapsedTime >= durationInSeconds)
        {
            _elapsedTime -= durationInSeconds;
            OnComplete?.Invoke(sender, args);
        }
    }
}

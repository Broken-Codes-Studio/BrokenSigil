namespace BrokenSigilCollection.Utility;

using Godot;

public static class SigilFactory
{
    /// <summary>
    /// Creates a timer.
    /// </summary>
    /// <param name="waitTime">The wait time.</param>
    /// <param name="oneShot">Whether the timer is one-shot.</param>
    /// <param name="autoStart">Whether the timer starts automaticly.</param>
    /// <param name="ignoreTimeScale">Whether the timer ignore the time scale.</param>
    /// <param name="name">The name of the timer.</param>
    /// <returns>The created timer.</returns>
    public static Timer CreateTimer(float waitTime, bool oneShot = true, bool autoStart = false, bool ignoreTimeScale = false, string name = nameof(Timer))
    {
        Timer timer = new();
        timer.Name = name;
        timer.OneShot = oneShot;
        timer.Autostart = autoStart;
        timer.IgnoreTimeScale = ignoreTimeScale;
        timer.WaitTime = waitTime;

        return timer;
    }
}

namespace BrokenSigilCollection.Abilities;

using Interface;
using Utility;

using Godot;

/// <summary>
/// Abstract base class for abilities with a duration.
/// </summary>
public abstract partial class DurationalAbility : CooldownAbility, IDuration
{

    #region Signals
    [Signal]
    public delegate void OnDurationChangeEventHandler(float totalTime, float timeLeft);
    [Signal]
    public delegate void OnDurationStartEventHandler();
    [Signal]
    public delegate void OnDurationEndEventHandler();
    #endregion

    /// <summary>
    /// Gets or sets the duration of the ability.
    /// </summary>
    public abstract float Duration { get; set; }

    /// <summary>
    /// Gets a value indicating whether the ability is currently being performed.
    /// </summary>
    public bool Performing { get; protected set; } = false;

    // Timer for handling duration.
    protected Timer durationTimer;

    public override void _Ready()
    {
        durationTimer = SigilFactory.CreateTimer(Duration, name: "DurationTimer");
        durationTimer.Timeout += AbilityTimeout;
        AddChild(durationTimer);

        base._Ready();
    }

    public override bool Perform(double delta)
    {
        getDurationTimeLeft();
        getCooldownTimeLeft();

        if (CheckCondition())
        {
            performAction(delta);
            return true;
        }

        return false;
    }

    public override bool CheckCondition()
    {
        if (OnCoolDown)
            return false;

        if (Performing)
            return true;

        if (CheckInput())
        {
            startDuration();
            return true;
        }

        return false;
    }
    /// <summary>
    /// Called when the duration timer times out.
    /// </summary>
    protected virtual void AbilityTimeout()
    {
        cancelAction();
    }

    /// <summary>
    /// Cancels the ability and stops performing.
    /// </summary>
    protected override void cancelAction()
    {
        stopDuration();

        base.cancelAction();
    }

    protected void startDuration()
    {
        Performing = true;
        durationTimer.Start();

        EmitSignal(SignalName.OnDurationStart);
    }

    protected void stopDuration()
    {
        durationTimer.Stop();
        EmitSignal(SignalName.OnDurationEnd);

        Performing = false;
    }

    /// <summary>
    /// Gets the duration time left.
    /// </summary>
    protected void getDurationTimeLeft()
    {
        if (durationTimer.TimeLeft >= 0)
            EmitSignal(SignalName.OnDurationChange, durationTimer.WaitTime, durationTimer.TimeLeft);
    }

}

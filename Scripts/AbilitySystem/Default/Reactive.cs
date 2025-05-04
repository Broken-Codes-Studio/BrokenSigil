namespace BrokenSigilCollection.Abilities;

using Interface;
using Utility;

using Godot;

/// <summary>
/// Abstract base class for passives that repeat an action when they are not on cooldown.
/// </summary>
[Icon("uid://cpqsus2rvuw21")]
public abstract partial class Reactive : Passive, ICooldown
{
    #region Signals
    [Signal]
    public delegate void OnCooldownChangeEventHandler(float totalTime, float timeLeft);
    [Signal]
    public delegate void OnCooldownStartEventHandler();
    [Signal]
    public delegate void OnCooldownEndEventHandler();
    #endregion

    /// <summary>
    /// Gets or sets the cooldown duration.
    /// </summary>
    public abstract float Cooldown { get; set; }

    /// <summary>
    /// Gets a value indicating whether the ability is on cooldown.
    /// </summary>
    public bool OnCoolDown { get; protected set; } = false;

    /// <summary>
    /// Timer node for handling cooldown.
    /// </summary>
    protected Timer coolDownTimer;

    /// <summary>
    /// Called when the node is added to the scene.
    /// </summary>
    public override void _Ready()
    {
        coolDownTimer = SigilFactory.CreateTimer(Cooldown, name: "CoolDownTimer");
        coolDownTimer.Timeout += coolDownFinished;
        AddChild(coolDownTimer);

        base._Ready();

        OnCoolDown = false;
    }

    /// <summary>
    /// Performs the reactive ability.
    /// </summary>
    /// <param name="delta">The delta time.</param>
    public override bool Perform(double delta)
    {
        getCooldownTimeLeft();

        if (!base.Perform(delta))
            return false;

        startCooldown();

        return true;
    }

    /// <summary>
    /// Checks the condition for the reactive ability.
    /// </summary>
    /// <returns>True if the condition is met, otherwise false.</returns>
    public override bool CheckCondition() => !OnCoolDown;

    protected virtual void cancelAction()
    {
        startCooldown();
    }

    /// <summary>
    /// starts the cooldown.
    /// </summary>
    protected virtual void startCooldown()
    {
        OnCoolDown = true;

        EmitSignal(SignalName.OnCooldownStart);

        coolDownTimer.Start();
    }

    /// <summary>
    /// Called when the cooldown timer finishes.
    /// </summary>
    protected virtual void coolDownFinished()
    {
        OnCoolDown = false;

        EmitSignal(SignalName.OnCooldownEnd);
    }

    /// <summary>
    /// Gets the cooldown time left.
    /// </summary>
    protected void getCooldownTimeLeft()
    {
        if (coolDownTimer.TimeLeft >= 0)
            EmitSignal(SignalName.OnCooldownChange, coolDownTimer.WaitTime, coolDownTimer.TimeLeft);
    }

}

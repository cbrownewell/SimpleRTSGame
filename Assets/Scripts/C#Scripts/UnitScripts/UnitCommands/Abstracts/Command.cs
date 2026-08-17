using System;

public abstract class Command
{
    /// <summary>
    /// Event fires when the command is canceled
    /// </summary>
    public event Action CommandCanceled;

    /// <summary>
    /// Executes core functionality of Command
    /// </summary>
    /// <returns>True if Command was successful.  False is there was an interuption</returns>
    public abstract void Execute();
    /// <summary>
    /// Returns whether the Command has finished
    /// </summary>
    public abstract bool IsFinished { get; }
    /// <summary>
    /// Stops/cancels the current Commands
    /// </summary>
    public virtual void Cancel() 
    {
        CommandCanceled?.Invoke();
    }
}

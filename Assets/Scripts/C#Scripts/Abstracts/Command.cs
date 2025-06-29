public abstract class Command
{
    /// <summary>
    /// Executes core functionality of Command
    /// </summary>
    /// <returns>True if Command was successful.  False is there was an interuption</returns>
    public abstract void Execute();
    /// <summary>
    /// Returns whether the Command has finished
    /// </summary>
    public abstract bool isFinished { get; }
    /// <summary>
    /// Clones the current command
    /// </summary>
    /// <returns></returns>
    //public abstract Command Clone();

}

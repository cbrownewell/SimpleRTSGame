using System.Collections.Generic;

public interface ICommandQueue
{
    /// <summary>
    /// Add current command to the end of the queue
    /// </summary>
    /// <param name="cmd"></param>
    void AddCommandToQueue(Command cmd);
    void AddCommandToQueue(List<Command> cmds);
    /// <summary>
    /// Overwrites current command and clears the queue
    /// </summary>
    /// <param name="cmd"></param>
    void AddCommand(Command cmd);
}

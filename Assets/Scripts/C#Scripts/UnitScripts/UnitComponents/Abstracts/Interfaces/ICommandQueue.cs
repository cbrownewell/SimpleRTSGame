using System.Collections.Generic;

public interface ICommandQueue
{
    /// <summary>
    /// Add current command to the end of the queue
    /// </summary>
    /// <param name="cmd"></param>
    void AddCommandToQueue(Command cmd);
    /// <summary>
    /// Adds a series of commands to the Unit's command queue
    /// </summary>
    /// <param name="cmds"></param>
    void AddCommandToQueue(List<Command> cmds);
    /// <summary>
    /// Overwrites current command and clears the queue
    /// </summary>
    /// <param name="cmd"></param>
    void AddCommand(Command cmd);

    /// <summary>
    /// Returns the type of the currently processing command 
    /// </summary>
    System.Type PeekCurrentCommandType();

    /// <summary>
    /// 
    /// </summary>
    int CommandsInQueue();
}

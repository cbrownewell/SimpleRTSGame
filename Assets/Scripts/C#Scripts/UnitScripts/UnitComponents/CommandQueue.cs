using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CommandQueue : MonoBehaviour, ICommandQueue
{
    public string CurrentCommand => PeekCurrentCommandType().ToString();
    public string QueueSize => CommandsInQueue().ToString();

    Queue<Command> _commandQueue = new Queue<Command>();
    Command _currentCommand;

    public void AddCommand(Command cmd)
    {
        _commandQueue.Clear();
        _commandQueue.Enqueue(cmd);
        _currentCommand.Cancel();
        _currentCommand = null;
    }

    public void AddCommandToQueue(Command cmd) => _commandQueue.Enqueue(cmd);
    public Type PeekCurrentCommandType() => _currentCommand.GetType();
    public int CommandsInQueue() => _commandQueue.Count;
    public void AddCommandToQueue(List<Command> cmds)
    {
        foreach (Command cmd in cmds)
            _commandQueue.Enqueue(cmd);
    }

    /// <summary>
    /// Processes and executes queued Commands
    /// </summary>
    protected void ProcessCommands()
    {
        if (_currentCommand != null && _currentCommand.isFinished == false)
        {
            return;
        }

        if (_commandQueue.Any() == false)
        {
            _commandQueue.Enqueue(new IdleCommand());
            return;
        }

        _currentCommand = _commandQueue.Dequeue();
        _currentCommand.Execute();
    }

    void Update() 
    {
        ProcessCommands();
    }


}

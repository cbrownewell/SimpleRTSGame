using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CommandQueue : MonoBehaviour, ICommandQueue
{
    public string CurrentCommand;
    public string QueueSize;

    Queue<Command> _commandQueue = new();
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
        if (_currentCommand != null && _currentCommand.IsFinished == false)
        {
            return;
        }

        if (_commandQueue.Any() == false)
        {
            _commandQueue.Enqueue(new IdleCommand());
            return;
        }

        QueueSize = CommandsInQueue().ToString();
        _currentCommand = _commandQueue.Dequeue();
        CurrentCommand = PeekCurrentCommandType().ToString();
        
        _currentCommand.Execute();
    }

    void Update() 
    {
        ProcessCommands();
    }


}

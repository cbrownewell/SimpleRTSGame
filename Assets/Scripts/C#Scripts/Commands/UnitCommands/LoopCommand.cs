using System.Collections.Generic;

public class LoopCommand : Command
{
    public override bool isFinished => _count == _commands.Count;
    public List<Command> _commands;
    int _count;

    ICommandQueue _queue;

    public LoopCommand(ICommandQueue queue, List<Command> cmdSeq) 
    {
        _queue = queue;
        _commands = cmdSeq;
    }

    public override void Execute()
    {
        _count = 0;
        foreach (var cmd in _commands) 
        { 
            _queue.AddCommandToQueue(cmd);
            _count++;
        }
    }
}

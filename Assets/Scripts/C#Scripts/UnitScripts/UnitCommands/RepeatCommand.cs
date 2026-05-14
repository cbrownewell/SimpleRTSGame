using System.Collections.Generic;

/// <summary>
/// Used to repeat commands.  Any commands preceding this one will be repeated.
/// </summary>
public class RepeatCommand : Command
{
    public override bool IsFinished => _count == _cmds.Count;
    public List<Command> _cmds;
    int _count;

    ICommandQueue _queue;

    public RepeatCommand(ICommandQueue queue, List<Command> cmds) 
    {
        _queue = queue;
        _cmds = cmds;
    }

    public override void Execute()
    {
        _count = 0;
        foreach (var cmd in _cmds) 
        { 
            _queue.AddCommandToQueue(cmd);
            _count++;
        }
        _queue.AddCommandToQueue(this);
    }

    public override void Cancel()
    {
        //throw new System.NotImplementedException();
    }
}

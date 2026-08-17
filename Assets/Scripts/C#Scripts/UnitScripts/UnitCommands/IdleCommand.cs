/// <summary>
/// A command that does nothing. Used to fill the time between commands.
/// </summary>
public class IdleCommand : Command
{
    public override bool IsFinished => true;

    public override void Cancel()
    {
        //Do nothing
    }

    public override void Execute()
    {
        //Do nothing
    }
}

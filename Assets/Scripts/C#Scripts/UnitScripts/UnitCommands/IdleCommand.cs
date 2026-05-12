using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleCommand : Command
{
    public override bool isFinished => true;

    public override void Cancel()
    {
        //Do nothing
    }

    public override void Execute()
    {
        //Do nothing
    }
}

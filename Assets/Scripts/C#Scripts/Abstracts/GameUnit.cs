using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public abstract class GameUnit : MonoBehaviour, ICommandQueue, IMoveable<Vector3>, ISelectable
{
    [SerializeField]
    protected float speed = 0;

    [SerializeField]
    SpriteRenderer selectCircle;

    [SerializeField]
    GameObject model;

    [SerializeField]
    protected NavMeshAgent _agent;
    public NavMeshAgent NavMeshAgent { get { return _agent; } }

    Queue<Command> _commandQueue = new Queue<Command>();
    Command _currentCommand;

    protected void ProcessCommands()
    {
        //Debug.Log($"Commands left in queue {_commandQueue.Count}");
        if (_currentCommand != null && _currentCommand.isFinished == false) 
        {
            //Debug.Log(_currentCommand.GetType().ToString());
            return;
        }

        if (_commandQueue.Any() == false)
            return;

        _currentCommand = _commandQueue.Dequeue();
        _currentCommand.Execute();
        Debug.Log(_currentCommand.GetType().ToString());
    }

    public void AddCommand(Command cmd)
    {
        _commandQueue.Clear();
        _commandQueue.Enqueue(cmd);
        _currentCommand = null;
    }
    public void AddCommandToQueue(Command cmd) => _commandQueue.Enqueue(cmd);
    public void MoveTo(Vector3 target) => _agent.SetDestination(target);
    public void Highlight(bool on) => selectCircle.enabled = on;
    public void ToggleModel(bool onOff) => model.SetActive(onOff);
    public void AddCommandToQueue(List<Command> cmds)
    {
        foreach (Command cmd in cmds) 
        {
            _commandQueue.Enqueue(cmd);
        }
    }
}

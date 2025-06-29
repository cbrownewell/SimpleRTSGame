using UnityEngine;

public abstract class GameBuilding : MonoBehaviour
{
    [SerializeField]
    Transform entryPoint;
    public Vector3 EntryPointCoords { get { return entryPoint.position; } }




}

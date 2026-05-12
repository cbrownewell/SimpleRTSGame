using UnityEngine;

public class ServiceLocator : MonoBehaviour
{
    public static ServiceLocator Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    [SerializeField]
    public UnitManager UnitManager;
    public UnitManager GetUnitManager() { return UnitManager; }

    [SerializeField]
    public SelectionManager SelectionManager;
    public SelectionManager GetSelectionManager() { return SelectionManager; }

}

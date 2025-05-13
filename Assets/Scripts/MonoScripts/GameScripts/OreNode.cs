using UnityEngine;

public class OreNode : MonoBehaviour
{
    public ResourceContainer container;

    [SerializeField]
    int regenAmount;
    [SerializeField]
    int totalAmount;

    void Start()
    {
        container = new ResourceContainer(totalAmount, regenAmount);
    }
}

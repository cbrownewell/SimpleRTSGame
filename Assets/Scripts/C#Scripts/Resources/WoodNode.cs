using UnityEngine;

public class WoodNode : RegenContainer
{
    [SerializeField]
    GameObject model;


    void ChangeVisual() 
    {
        if (chargeCount is 0)
            model.gameObject.SetActive(false);
        else 
            model.gameObject.SetActive(true);
    }


    private void Update()
    {
        ChangeVisual();
    }


}

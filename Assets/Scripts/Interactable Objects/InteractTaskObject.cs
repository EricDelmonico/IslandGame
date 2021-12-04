using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractTaskObject : InteractableBase
{
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        
    }

    // Update is called once per frame
    new void Update()
    {
        currentObj = manager.currentObjective;
        base.Update();
    }

    public override void Interacted()
    {
        if(gameObject.CompareTag(currentObj.objectTag))
        {
            if (currentObj.amountToCollect > 0)
            {
                currentObj.amountToCollect--;
                manager.currentObjective.amountToCollect = currentObj.amountToCollect;
            }
               

        }
        base.Interacted();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractTaskObject : InteractableBase
{
    private GameManager manager;
    private DayData data;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        manager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        data = manager.currentDayData;
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
    }

    public override void Interacted()
    {
        for(int i = 0; i < data.objectives.Length; i++)
        {
            if(gameObject.CompareTag(data.objectives[i].objectTag))
            {
                data.objectives[i].amountToCollect--;

                if (data.objectives[i].amountToCollect <= 0)
                    data.objectives[i].completed = true;

                break;
            }
        }
        base.Interacted();
    }
}

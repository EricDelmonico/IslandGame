using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndCollectObjective : MonoBehaviour
{
    private GameManager manager;
    private DayData.Objective obj;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        obj = manager.currentObjective;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!obj.bottleObjective && obj.amountToCollect <= 0)
            manager.currentObjective.completed = true;
    }
}

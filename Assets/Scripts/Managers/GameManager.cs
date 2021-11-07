using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int currentDay = 1;

    [SerializeField] private DayData[] dayData;

    [HideInInspector] public DayData currentDayData;

    [HideInInspector]public DayData.Objective currentObjective;

    private int objectiveIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        currentDayData = dayData[0];
        currentObjective = currentDayData.objectives[objectiveIndex];
        //currentDayData.objectives[0].completed = true;
    }

    // Update is called once per frame
    void Update()
    {
        //currentDayData = dayData[currentDay - 1];
        //Debug.Log(currentDayData.day);

        currentDayData = dayData[currentDay - 1];

        Debug.Log("Index: " + objectiveIndex + ", " + currentObjective.completed);
        if(currentObjective.completed)
        {
            if( objectiveIndex < currentDayData.objectives.Length)
            {
                objectiveIndex++;
                currentObjective = currentDayData.objectives[objectiveIndex];
            }
        }
        //Update current objective
        //for(int i = 0; i < currentDayData.objectives.Length; i++)
        //{

        //    if(!currentDayData.objectives[i].completed)
        //    {
        //        currentObjective = currentDayData.objectives[i];

        //        //currentDayData.objectives[i] = currentObjective;
        //        break;
        //    }
        //}

        //Change the day once the last objective is completed
        int lastIndex = currentDayData.objectives.Length - 1;
        Debug.Log("Last index: " + lastIndex);

        if (currentDayData.objectives[lastIndex].completed)
            IncreaseDay();
    }

    public void IncreaseDay() => currentDay++;

}

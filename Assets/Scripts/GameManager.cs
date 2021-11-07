using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int currentDay = 1;

    [SerializeField] private DayData[] dayData;

    public DayData currentDayData;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentDayData = dayData[currentDay - 1];
    }

    public void IncreaseDay() => currentDay++;
}

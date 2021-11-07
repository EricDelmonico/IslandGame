using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Day", menuName = "Day")]
public class DayData : ScriptableObject
{
    [Serializable]
    public struct Objective
    {
        public string objectTag;
        public string description;
        public int amountToCollect;

        public bool bottleObjective;
        public bool completed;
    }

    public int day;

    public Text morningNote;
    public Text eveningNote;

    public Objective[] objectives;
}
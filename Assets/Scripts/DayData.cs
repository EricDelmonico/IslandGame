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
        public int amountToCollect;
    }

    public int day;

    public Text morningNote;
    public Text eveningNote;

    public Objective[] objectives;
}
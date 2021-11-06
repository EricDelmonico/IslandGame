using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Day", menuName = "Day")]
public class DayData : ScriptableObject
{
    public int day;

    public Text morningNote;
    public Text eveningNote;
}

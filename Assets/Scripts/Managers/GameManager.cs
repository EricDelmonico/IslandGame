using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int currentDay = 1;

    public DayData[] dayData;

    [HideInInspector] public DayData currentDayData;

    [HideInInspector]public DayData.Objective currentObjective;

    [SerializeField] private DayNightCycle dayNightCycleScript;
    [SerializeField] private GameObject player;
    [SerializeField] private Camera nightCamera;
    [SerializeField] private GameObject bottle;
    [SerializeField] private GameObject emptyBottle;
    [SerializeField] private GameObject campfire;
    private Vector3 nightCamOriginalPosition;
    private Vector3 playerCamOriginalPosition;
    private Quaternion nightCamOriginalRotation;
    private Quaternion playerCamOriginalRotation;
    private float camAnimationSeconds = 3.0f;
    private float currentCamAnimSeconds = 0;

    private int objectiveIndex = 0;

    private bool nightTime;

    private GameObject interactText;
    private GameObject objectiveText;

    // Start is called before the first frame update
    void Start()
    {
        interactText = GameObject.Find("InteractText");
        objectiveText = GameObject.Find("Objective Text");
        Time.timeScale = 1;
        currentDayData = dayData[0];
        currentObjective = currentDayData.objectives[objectiveIndex];
        nightTime = false;
        nightCamOriginalPosition = nightCamera.transform.position;
        nightCamOriginalRotation = nightCamera.transform.rotation;
        //currentDayData.objectives[0].completed = true;
    }

    public static float InOut(float k)
    {
        if ((k *= 2f) < 1f) return 0.5f * k * k;
        return -0.5f * ((k -= 1f) * (k - 2f) - 1f);
    }

    // Update is called once per frame
    void Update()
    {
        //currentDayData = dayData[currentDay - 1];

        if (nightTime && currentCamAnimSeconds < 1)
        {
            currentCamAnimSeconds += Time.deltaTime / camAnimationSeconds;
            float easedSeconds = InOut(currentCamAnimSeconds);
            nightCamera.transform.position = Vector3.Lerp(playerCamOriginalPosition, nightCamOriginalPosition, easedSeconds);
            nightCamera.transform.rotation = Quaternion.Lerp(playerCamOriginalRotation, nightCamOriginalRotation, easedSeconds);
        }
        else if (!nightTime && currentCamAnimSeconds > 0)
        {
            currentCamAnimSeconds -= Time.deltaTime / (camAnimationSeconds / 3);
            float easedSeconds = InOut(currentCamAnimSeconds);
            nightCamera.transform.position = Vector3.Lerp(playerCamOriginalPosition, nightCamOriginalPosition, easedSeconds);
            nightCamera.transform.rotation = Quaternion.Lerp(playerCamOriginalRotation, nightCamOriginalRotation, easedSeconds);
        }
        else if (currentCamAnimSeconds < 1 && !player.activeSelf)
        {
            player.SetActive(true);
            interactText.SetActive(true);
            objectiveText.SetActive(true);
            nightCamera.gameObject.SetActive(false);
        }

        // Do nothing at night
        if (nightTime) return;

        //Debug.Log("Index: " + objectiveIndex + ", " + currentObjective.completed);
        if (currentObjective.completed)
        {
            objectiveIndex++;

            if ( objectiveIndex < currentDayData.objectives.Length)
            {               
                currentObjective = currentDayData.objectives[objectiveIndex];
            }
            else
            {
                objectiveIndex = 0;
                IncreaseDay();
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
        //Debug.Log("Last index: " + lastIndex);

        //if (currentDayData.objectives[lastIndex].completed)
            //IncreaseDay();
    }

    private float dayTimeSpeedUp = 50;
    private void IncreaseDay()
    {
        playerCamOriginalPosition = Camera.main.transform.position;
        playerCamOriginalRotation = Camera.main.transform.rotation;
        currentCamAnimSeconds = 0;

        currentDay++;
        nightTime = true;
        Debug.Log(currentDay);
        if (currentDay > 4 && !campfire.activeSelf)
        {
            campfire.SetActive(true);
            dayNightCycleScript.dayCycles = 3;
        }
        player.SetActive(false);
        nightCamera.gameObject.SetActive(true);
        interactText.SetActive(false);
        objectiveText.SetActive(false);

        dayNightCycleScript.dayTime /= dayTimeSpeedUp;
        dayNightCycleScript.Sunrise += DayNightCycleScript_Sunrise;
    }

    private void DayNightCycleScript_Sunrise()
    {
        dayNightCycleScript.dayTime *= dayTimeSpeedUp;
        NextDay();
        nightTime = false;

        bottle.SetActive(true);
        emptyBottle.SetActive(false);

        dayNightCycleScript.Sunrise -= DayNightCycleScript_Sunrise;
    }

    private void NextDay()
    {
        currentDayData = dayData[currentDay - 1];
        currentObjective = currentDayData.objectives[objectiveIndex];
    }

}

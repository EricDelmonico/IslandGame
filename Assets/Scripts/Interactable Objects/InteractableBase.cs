using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableBase : MonoBehaviour
{
    [SerializeField] protected AudioClip clip;
    protected AudioSource source;

    protected GameManager manager;
    protected DayData data;
    protected DayData.Objective currentObj;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        source = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();
        source.loop = false;

        manager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        data = manager.currentDayData;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (data != manager.currentDayData)
            data = manager.currentDayData;

        currentObj = manager.currentObjective;
    }

    public virtual void Interacted()
    {
        source.PlayOneShot(clip);
        gameObject.SetActive(false);
    }
}

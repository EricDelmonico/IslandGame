using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [SerializeField]
    private float dayTime = 30f;
    private float currentAngle;

    private GameObject sun;

    private void Start()
    {
        currentAngle = 0;
        sun = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        float angleStep = 360.0f * Time.deltaTime / dayTime;

        currentAngle = (currentAngle + angleStep) % 360.0f;
        sun.GetComponent<Light>().intensity = currentAngle <= 190 ? 1 : 0.01f;

        transform.rotation = Quaternion.AngleAxis(currentAngle, transform.right);
    }
}

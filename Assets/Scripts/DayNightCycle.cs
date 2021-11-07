using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public float dayTime = 3600f;
    private float previousAngle;
    private float currentAngle;

    private GameObject sun;

    public event System.Action Sunrise;

    private void Start()
    {
        currentAngle = 30;
        previousAngle = currentAngle;
        sun = transform.GetChild(0).GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        float angleStep = 360.0f * Time.deltaTime / dayTime;

        currentAngle = (currentAngle + angleStep) % 360.0f;
        sun.GetComponent<Light>().intensity = currentAngle <= 190 ? 1 : 0.01f;

        if (previousAngle < 30 && currentAngle >= 30) Sunrise?.Invoke();

        transform.rotation = Quaternion.AngleAxis(currentAngle, transform.right);
        previousAngle = currentAngle;
    }
}

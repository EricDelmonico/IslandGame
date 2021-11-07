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

    public int dayCycles;

    private void Start()
    {
        currentAngle = 60;
        previousAngle = currentAngle;
        sun = transform.GetChild(0).GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        float angleStep = 360.0f * Time.deltaTime / dayTime;

        currentAngle = (currentAngle + angleStep) % 360.0f;
        sun.GetComponent<Light>().intensity = currentAngle <= 190 ? 1 : 0.01f;

        // Player wakes
        if (previousAngle < 60 && currentAngle >= 60)
        {
            dayCycles--;
            if (dayCycles <= 0)
            {
                dayCycles = 0;
                Sunrise?.Invoke();
                RenderSettings.ambientIntensity = 1;
            }
        }

        // Exact sunrise
        if (previousAngle < 340 && currentAngle >= 340)
        {
            RenderSettings.ambientIntensity = 0;
            StopAllCoroutines();
            StartCoroutine(SetAmbientOverTime(0, 1));
        }
        // Exact sunset
        if (previousAngle < 190 && currentAngle >= 190)
        {
            RenderSettings.ambientIntensity = 1;
            StopAllCoroutines();
            StartCoroutine(SetAmbientOverTime(0, -1));
        }

        transform.localRotation = Quaternion.AngleAxis(currentAngle, transform.right);
        previousAngle = currentAngle;
    }

    private float ambientReturnTime = 1.5f;
    private int ambientReturnIterations = 100;
    IEnumerator SetAmbientOverTime(int iteration, float dir)
    {
        RenderSettings.ambientIntensity += dir / ambientReturnIterations;
        iteration++;
        yield return new WaitForSeconds(ambientReturnTime / ambientReturnIterations);
        if (iteration < ambientReturnIterations)
        {
            StartCoroutine(SetAmbientOverTime(iteration, dir));
        }
    }
}

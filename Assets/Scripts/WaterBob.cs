using UnityEngine;

public class WaterBob : MonoBehaviour
{
    [SerializeField]
    private float height = 0.1f;

    [SerializeField]
    private float period = 1;

    private Vector3 initialPosition;
    private float offset;

    private void Awake()
    {
        initialPosition = transform.position;
        offset = 1 - (Random.value * 2);
    }

    private void Update()
    {
        transform.position = initialPosition - Vector3.up * Mathf.Sin((Time.time + offset) * period) * height;
    }
}

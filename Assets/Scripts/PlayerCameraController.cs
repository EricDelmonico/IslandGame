using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    [SerializeField]
    private float lookSpeed = 200;
    [SerializeField]
    private float yAxisLimit = 85;

    private float rotationX = 0;
    private float rotationY = 0;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        rotationX += Input.GetAxis("Mouse X") * lookSpeed * Time.deltaTime;
        rotationY += Input.GetAxis("Mouse Y") * lookSpeed * Time.deltaTime;
        rotationY = Mathf.Clamp(rotationY, -yAxisLimit, yAxisLimit);

        Quaternion yaw = Quaternion.AngleAxis(rotationX, transform.up);
        Quaternion pitch = Quaternion.AngleAxis(-rotationY, transform.right);

        Camera.main.transform.localRotation = yaw * pitch;
    }
}

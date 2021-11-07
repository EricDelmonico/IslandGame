using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 5;

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = new Vector3(0, 0, 0);
        var cForward = Camera.main.transform.forward;
        cForward.y = 0;
        var cRight = Camera.main.transform.right;
        cRight.y = 0;
        if (Input.GetKey(KeyCode.W)) direction += cForward;
        if (Input.GetKey(KeyCode.A)) direction -= cRight;
        if (Input.GetKey(KeyCode.S)) direction -= cForward;
        if (Input.GetKey(KeyCode.D)) direction += cRight;
        direction.Normalize();
        transform.position += direction * Time.deltaTime * speed;
    }
}

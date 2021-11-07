using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    [SerializeField] private Camera mainCam;
    [SerializeField] private float range;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) interact = true;
    }

    private bool interact = false;
    private void FixedUpdate()
    {
        RaycastHit hit;

        var origin = mainCam.transform.position;
        var direction = mainCam.transform.forward;
        if (Physics.Raycast(origin, direction * range, out hit, range))
        {
            Debug.DrawRay(origin, direction * hit.distance, Color.green);
            if (interact)
            {
                hit.collider.gameObject.GetComponent<InteractableBase>()?.Interacted();
            }
        }
        else
        {
            Debug.DrawRay(origin, direction * range, Color.red);
        }

        if (interact) interact = false;
    }

}

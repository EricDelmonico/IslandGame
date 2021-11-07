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
        
    }

    private void FixedUpdate()
    {
        RaycastHit hit;

        if (Physics.Raycast(mainCam.transform.position, transform.TransformDirection(Vector3.forward), out hit, range))
        {
            Debug.DrawRay(mainCam.transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.green);
            hit.collider.gameObject.GetComponent<InteractableBase>()?.Interacted();
        }
        else
        {
            Debug.DrawRay(mainCam.transform.position, transform.TransformDirection(Vector3.forward) * range, Color.red);
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interact : MonoBehaviour
{
    [SerializeField] private Camera mainCam;
    [SerializeField] private float range;
    [SerializeField] private Text interactText;

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
            if (interactText != null)
            {
                // Untagged should not display any text
                interactText.text = hit.collider.gameObject.CompareTag("Untagged") ? "" : hit.collider.gameObject.tag;
            }
        }
        else
        {
            if (interactText != null) interactText.text = string.Empty;
            Debug.DrawRay(origin, direction * range, Color.red);
        }

        if (interact) interact = false;
    }

}

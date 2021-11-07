using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableBase : MonoBehaviour
{
    [SerializeField] protected AudioClip clip;
    protected AudioSource source;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        source = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();
        source.loop = false;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }

    public virtual void Interacted()
    {
        source.PlayOneShot(clip);
        gameObject.SetActive(false);
    }
}

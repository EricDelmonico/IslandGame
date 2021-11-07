using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableBase : MonoBehaviour
{
    [SerializeField] protected AudioClip clip;
    AudioSource source;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        source = gameObject.AddComponent<AudioSource>();
        source.loop = false;
        source.clip = clip;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }

    public virtual void Interacted()
    {
        source.Play();
        gameObject.SetActive(false);
    }
}

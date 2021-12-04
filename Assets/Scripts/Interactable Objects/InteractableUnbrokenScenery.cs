using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableUnbrokenScenery : InteractableBase
{
    [SerializeField]
    private GameObject postInteractPrefab;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
    }

    public override void Interacted()
    {
        var obj = Instantiate(postInteractPrefab, transform.position, transform.rotation);
        obj.transform.localScale = transform.localScale;
        base.Interacted();
    }
}

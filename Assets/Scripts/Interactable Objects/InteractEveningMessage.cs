using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractEveningMessage : InteractableBase
{
    [SerializeField] private GameObject notePanel;
    [SerializeField] private Text textPos;

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
        textPos.text = data.eveningNote.text;
        notePanel.SetActive(true);
        Cursor.visible = true;

        base.Interacted();
    }
}

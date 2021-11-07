using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject messagePanel;
    [SerializeField] private Text currentObjText;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = gameObject.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            ClosePanel();

        UpdateObjectiveText(gameManager.currentObjective);
    }

    public void ClosePanel()
    {
        if (messagePanel.activeSelf)
        {
            Time.timeScale = 1;
            messagePanel.SetActive(false);
            gameManager.currentObjective.completed = true;
            Cursor.visible = false;
        }
    }

    public void UpdateObjectiveText(DayData.Objective obj)
    {
        string newText = "";

        if (!obj.bottleObjective)
        {
            if (obj.amountToCollect > 0)
                newText = obj.description + ": " + obj.amountToCollect + " left";
            else
                newText = "Return items to the flag";
        }
        else
        {
            newText = obj.description;
        }

        currentObjText.text = newText;
    }
}

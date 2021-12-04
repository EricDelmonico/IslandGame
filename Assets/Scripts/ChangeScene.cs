using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    private void Start()
    {
        if(SceneManager.GetActiveScene().name == "EndMenu")
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
        }
    }

    public void ChangingScene(string name, bool lockMouse)
    {
        Debug.Log("Changing");

        SceneManager.LoadScene(name);
    }

    public void ChangingScene(string name)
    {
        Debug.Log("Changing");

        SceneManager.LoadScene(name);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}

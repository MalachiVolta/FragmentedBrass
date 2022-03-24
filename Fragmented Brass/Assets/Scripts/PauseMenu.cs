using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public Image img;
    public GameObject ResumeButton;
    public GameObject QuitButton;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            img.enabled = true;
            Time.timeScale = 0;
            QuitButton.SetActive(true);
            ResumeButton.SetActive(true);
        }
    }
    public void ResumeGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        img.enabled = false;
        Time.timeScale = 1;
        Cursor.visible = false;
        img.enabled = false;
        QuitButton.SetActive(false);
        ResumeButton.SetActive(false);
    }

    // Update is called once per frame
    public void QuitGame()
    {
        Application.Quit();
    }
}

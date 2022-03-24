using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuButtons : MonoBehaviour
{
    public SceneHandler gameHandler;

    private void Start()
    {
        gameHandler = GameObject.Find("SceneHandler").GetComponent<SceneHandler>();
    }
    public void doExitGame()
    {
        Application.Quit();
    }

    public void doPlayGame()
    {
        gameHandler.LoadGame();
    }

}

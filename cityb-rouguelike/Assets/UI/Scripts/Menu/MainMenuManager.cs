using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public Button buttonContinue;
    public Button buttonNewGame;
    public Button buttonLoadGame;
    public Button buttonOptions;
    public Button buttonExit;

    void Awake()
    {
        buttonExit.onClick.AddListener(ExitButton);
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    Button newGameBtn;
    Button continueBtn;
    Button quitBtn;

    private void Awake()
    {
        newGameBtn = transform.GetChild(1).GetComponent<Button>();
        continueBtn = transform.GetChild(2).GetComponent<Button>();
        quitBtn = transform.GetChild(3).GetComponent<Button>();

        newGameBtn.onClick.AddListener(NewGame);
        continueBtn.onClick.AddListener(ContinueGame);
        quitBtn.onClick.AddListener(QuitGame);
    }

    void NewGame()
    {
        PlayerPrefs.DeleteAll();
        //load forest scene
        SceneController.Instance.TransitionToFirstLevel();
    }

    void ContinueGame()
    {
        //load forest scene
        SceneController.Instance.TransitionToLoadGame();
    }

    void QuitGame()
    {
        Application.Quit();
    }
}

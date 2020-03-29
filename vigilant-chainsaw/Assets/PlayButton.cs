using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    public Button playButton;
    public Button creditButton;
    public Button exitButton;
    // Start is called before the first frame update
    void Start()
    {
        playButton.onClick.AddListener(Play);
        creditButton.onClick.AddListener(GoToCredits);
        exitButton.onClick.AddListener(Exit);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Play()
    {
        SceneManager.LoadScene("Game");
    }

    public void GoToCredits()
    {
        SceneManager.LoadScene("CreditsScreen");
    }

    public void Exit()
    {
        Application.Quit();
    }
}

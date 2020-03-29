using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CreditScreen : MonoBehaviour
{
    public Button backButton;

    // Start is called before the first frame update
    void Start()
    {
        backButton.onClick.AddListener(Back);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Back()
    {
        SceneManager.LoadScene("MainMenu");
    }
}

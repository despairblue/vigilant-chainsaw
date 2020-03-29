using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public TMPro.TextMeshProUGUI highScoreHUD;
    public int currentScore = 0;
    public int currentLifes = 3;

    // Start is called before the first frame update
    void Start()
    {
        Render();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Render()
    {
        highScoreHUD.SetText(currentScore.ToString());
    }

    public void IncreaseScore(int delta)
    {
        currentScore += delta;

        Render();
    }

    public void ReduceLife()
    {
        currentLifes -= 1;
    }
}

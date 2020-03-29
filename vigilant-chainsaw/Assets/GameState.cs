﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public TMPro.TextMeshProUGUI highScoreHUD;
    public List<GameObject> lifes;
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

        for (int i = 0; i < lifes.Count; i++)
        {
            var go = lifes[i];
            var emptyHeart = go.transform.Find("Empty");
            var fullHeart = go.transform.Find("Full");

            if (i < currentLifes)
            {
                emptyHeart.gameObject.SetActive(false);
                fullHeart.gameObject.SetActive(true);
            }
            else
            {
                emptyHeart.gameObject.SetActive(true);
                fullHeart.gameObject.SetActive(false);
            }
        }
    }

    public void IncreaseScore(int delta)
    {
        currentScore += delta;
        Render();
    }

    public void ReduceLife()
    {
        currentLifes -= 1;
        Render();
    }
}

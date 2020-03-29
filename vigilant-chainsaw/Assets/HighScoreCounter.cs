using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScoreCounter : MonoBehaviour
{
    private GameState gameState;
    // Start is called before the first frame update
    void Start()
    {
        var gameStateGameObject = GameObject.FindGameObjectWithTag("GameState");
        gameState = gameStateGameObject.GetComponent<GameState>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Score"))
        {
            gameState.IncreaseScore(100);
            GameObject.Destroy(collider.gameObject);
        }
    }
}

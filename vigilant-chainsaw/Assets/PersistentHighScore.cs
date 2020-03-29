using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentHighScore : MonoBehaviour
{
    public int highScore = 0;
    // Start is called before the first frame update
    void Start()
    {
        Object.DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetHighScore(int score)
    {
        highScore = Mathf.Max(score, highScore);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameoverSceneUIController : MonoBehaviour
{
    public Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "SCORE: " + Scoreboard.score;
    }
}

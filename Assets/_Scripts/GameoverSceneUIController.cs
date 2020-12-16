/*******************
File name: GameoverSceneUIController.cs
Author: Shun min Hsieh
Student Number: 101212629
Date last Modified: 2020/12/13
Program description: A class for displaying the final score in the gameover scene.
Revision History:
2020/12/13
 - Added Start function

Class:
    GameoverSceneUIController
Functions:
    Start
*******************/
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

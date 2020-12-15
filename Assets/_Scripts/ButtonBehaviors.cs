/*******************
File name: ButtonBehaviors.cs
Author: Shun min Hsieh
Student Number: 101212629
Date last Modified: 2020/11/22
Program description: A class for button behaviors throughout the game.
Revision History:
2020/11/22
 - Added OnPlayButtonPressed function
 - Added OnInstructionsButtonPressed function
 - Added OnMenuButtonPressed function
 - Added OnGameoverButtonPressed function

Class:
    ButtonBehaviors
Functions:
    OnPlayButtonPressed
    OnInstructionsButtonPressed
    OnMenuButtonPressed
    OnGameoverButtonPressed
*******************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonBehaviors : MonoBehaviour
{
    public AudioSource buttonSound;
    public void OnPlayButtonPressed() // For buttons pointing to the play scene.
    {
        buttonSound.Play();
        SceneManager.LoadScene("GameScreen");
    }

    public void OnInstructionsButtonPressed() // For buttons pointing to the instructions scene.
    {
        buttonSound.Play();
        SceneManager.LoadScene("InstructionsScreen");
    }

    public void OnMenuButtonPressed() // For buttons pointing to the main menu.
    {
        buttonSound.Play();
        SceneManager.LoadScene("MenuScreen");
    }

    public void OnGameoverButtonPressed() // For testing purpose only
    {
        buttonSound.Play();
        SceneManager.LoadScene("GameoverScreen");
    }
}

﻿/*******************
File name: LifeCounter.cs
Author: Shun min Hsieh
Student Number: 101212629
Date last Modified: 2020/12/13
Program description: A class sets lives icons in the Play scene.
Revision History:
2020/12/13
 - Added Update function

Class:
    LifeCounter
Functions:
    Update
*******************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeCounter : MonoBehaviour
{
    public int indexNum;    
    public PlayerController player;

    void Update()
    {
        if (player.lives < indexNum)
        {
            Destroy(gameObject);
        }
    }
}

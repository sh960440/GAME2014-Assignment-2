/*******************
File name: PlayerAnimationType.cs
Author: Shun min Hsieh
Student Number: 101212629
Date last Modified: 2020/12/13
Program description: An enum includes different types of player animations.
Revision History:
2020/12/13
 - Added PlayerAnimationType enumeration

Enumeration:
    PlayerAnimationType
*******************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum PlayerAnimationType 
{
    IDLE,
    RUN,
    JUMP
}

/*******************
File name: PlayerCamera.cs
Author: Shun min Hsieh
Student Number: 101212629
Date last Modified: 2020/12/13
Program description: A class locks the z position of the main camera.
Revision History:
2020/12/13
 - Added Update function

Class:
    PlayerCamera
Functions:
    Update
*******************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, -10);
    }
}

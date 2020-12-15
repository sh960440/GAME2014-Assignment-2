/*******************
File name: BulletManager.cs
Author: Shun min Hsieh
Student Number: 101212629
Date last Modified: 2020/10/23
Program description: A class controls the creation and use of the bullet pool.
Revision History:
2020/10/23
 - Added Start function
 - Added _BuildBulletPool function
 - Added GetBullet function
 - Added ReturnBullet function

Class:
    BulletManager
Functions:
    Start
    _BuildBulletPool
    GetBullet
    ReturnBullet
*******************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public GameObject bullet;
    public int maxBullets;
    private Queue<GameObject> m_bulletPool;

    // Start is called before the first frame update
    void Start()
    {
        _BuildBulletPool();
    }

    private void _BuildBulletPool()
    {
        // create emtpy Queue structrue
        m_bulletPool = new Queue<GameObject>();

        for (int count = 0; count < maxBullets; count++)
        {
            var tempBullet = Instantiate(bullet);
            tempBullet.SetActive(false);
            tempBullet.transform.parent = transform;
            m_bulletPool.Enqueue(tempBullet);
        }
    }

    public GameObject GetBullet(Vector3 position, int direction)
    {
        var newBullet = m_bulletPool.Dequeue();
        newBullet.SetActive(true);
        newBullet.transform.position = position;
        newBullet.GetComponent<BulletController>().speed *= direction;
        return newBullet;
    }

    public void ReturnBullet(GameObject returnBullet)
    {
        returnBullet.SetActive(false);
        m_bulletPool.Enqueue(returnBullet);
    }
}
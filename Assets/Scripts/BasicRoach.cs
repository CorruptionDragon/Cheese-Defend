using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicRoach : MonoBehaviour
{
    private float health = 5;

    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D collision)
    {
        DecreaseHealth();
    }

    public void DecreaseHealth()
    {
        health--;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}

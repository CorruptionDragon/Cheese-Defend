using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicRoach : MonoBehaviour
{
    [SerializeField]
    private float health = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
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

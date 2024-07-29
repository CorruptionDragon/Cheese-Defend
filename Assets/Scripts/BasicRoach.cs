using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicRoach : MonoBehaviour
{
    [SerializeField]
    private float health = 10;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DecreaseHealth();
    }

    public void DecreaseHealth()
    {
        health--;

        StartCoroutine(BlinkRed());

        if (health <= 0)
        {
            GameObject.Find("Canvas").GetComponent<CurrencyManger>().IncreaseCurrency(10);
            Destroy(gameObject);
        }
    }

    IEnumerator BlinkRed()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.2f);
        GetComponent<SpriteRenderer>().color = Color.white;
    }
}

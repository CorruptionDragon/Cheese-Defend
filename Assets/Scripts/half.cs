using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour
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

      

        if (health <= 0)
        {
            Destroy(gameObject);
            SceneManager.LoadScene("LoseScene");
        }
    }
}

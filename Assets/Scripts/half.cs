using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField]
    public float health = 10;
    public Text text;

    void Update()
    {
        text.text = health.ToString();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.gameObject.CompareTag("Cockroach"))
        {
            health--;

            RampUpDiff.onEnemyDestroy.Invoke();
            Destroy(other.gameObject);
            if (health <= 0)
            {
                Destroy(gameObject);
                SceneManager.LoadScene("LoseScene");
            }
        }       

    }
    }

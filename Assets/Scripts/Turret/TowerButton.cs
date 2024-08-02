using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerButton : MonoBehaviour
{
    [SerializeField] public GameObject Tower = null;

    GameObject Canvas = null;

    private void Start()
    {
        if (Tower == null) { Debug.Log("No assigned Tower was found."); return; } //i knew one of y'all was gonna screw this up by accident so i added these checks
        Canvas = GameObject.Find("Canvas");
        if (Canvas == null) { Debug.Log("No Canvas was found."); return; }
        if (gameObject.GetComponent<Button>() == null) { Debug.Log("No Button was found attached to this GameObject."); return; }
        if (Canvas.GetComponent<TowerCreation>() == null) { Debug.Log("No TowerCreation was found attached to the Canvas."); return; }
        gameObject.GetComponent<Button>().onClick.AddListener(() => Canvas.GetComponent<TowerCreation>().StartPlacement(Tower));
    }

    //will add more functionality later, like viewing price amount and such. maybe cooler button effects
}

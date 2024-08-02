using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerCreation : MonoBehaviour
{
    GameObject TowersHolder = null;
    CurrencyManger CurrencyManger = null;

    GameObject PlacementObject = null;
    GameObject CurrentPlacement = null;

    bool CanPlace = false;

    private void Start()
    {
        TowersHolder = GameObject.Find("TowersHolder"); //making sure someone doesn't do a stupid
        if (TowersHolder == null) Debug.Log("No TowersHolder was found. Insert an empty into the scene and rename it to TowersHolder");
        else TowersHolder.transform.position = new Vector3(0, 0, 0);
        CurrencyManger = GameObject.FindObjectOfType<CurrencyManger>();
        if (CurrencyManger == null) Debug.Log("No CurrencyManager was found. Insert one into the canvas.");
    }

    void Update()
    {
        if (PlacementObject != null && TowersHolder != null)
        {
            if (Input.GetMouseButtonDown(1))
            {
                CurrentPlacement = null;
                Destroy(PlacementObject);
                return;
            }

            Vector2 playerMousePosition = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));

            bool found = false; //this collision detection feels really messy but it works
            bool InPlaceable = false;
            for (int i = 0; i < TowersHolder.transform.childCount; i++)
            { 
                Transform Child = TowersHolder.transform.GetChild(i);
                if (IsTouchingTower(PlacementObject.transform, Child) == true)
                {
                    found = true;
                    break;
                }
            }
            foreach (GameObject Child in GameObject.FindGameObjectsWithTag("TowerPlacement"))
            {
                if (found == true) break;

                if (IsTouchingObject(Child, PlacementObject) == true)
                {
                    InPlaceable = true;
                    break;
                }
            }
            if (found == false && InPlaceable == true) { 
                CanPlace = true; 
                ColorObject(PlacementObject, new Color(0, 255, 0, 0.75f));
            }
            else
            {
                CanPlace = false;
                ColorObject(PlacementObject, new Color(255, 0, 0, 0.75f));
            }

            if (Input.GetMouseButtonDown(0) && CanPlace)
            {
                PlaceSelected(playerMousePosition);
                return;
            }

            PlacementObject.transform.position = new Vector3(playerMousePosition.x, playerMousePosition.y, -1);
        };
    }

    void PlaceSelected(Vector2 playerMousePosition)
    {
        if (CurrencyManger.Currency >= CurrentPlacement.GetComponent<Turret>().Price)
        {
            CurrencyManger.IncreaseCurrency(-CurrentPlacement.GetComponent<Turret>().Price);

            GameObject NewTower = Instantiate(CurrentPlacement, new Vector3(playerMousePosition.x, playerMousePosition.y, -1), Quaternion.identity);
            NewTower.name = "Tower";
            NewTower.transform.parent = TowersHolder.transform;
        }

        CurrentPlacement = null;
        Destroy(PlacementObject);
    }

    public void StartPlacement(GameObject Tower)
    {
        if (PlacementObject != null) {
            CurrentPlacement = null;
            Destroy(PlacementObject);
        };

        if (CurrencyManger == null || CurrencyManger.Currency < Tower.GetComponent<Turret>().Price) return;

        CanPlace = false;
        CurrentPlacement = Tower;

        Vector2 playerMousePosition = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
        PlacementObject = Instantiate(CurrentPlacement, playerMousePosition, Quaternion.identity);
        PlacementObject.GetComponent<Turret>().enabled = false;
        PlacementObject.name = "TowerPlacement";
        ColorObject(PlacementObject, new Color(0, 255, 0, 0.75f));
    }

    void ColorObject(GameObject ToColor, Color ColorTo)
    {
        foreach (SpriteRenderer Child in ToColor.GetComponentsInChildren<SpriteRenderer>())
        {
            Child.color = ColorTo;
        }
    }

    bool IsTouchingObject(GameObject First, GameObject Second) //no idea how well this works
    {
        float Size = (Second.transform.localScale.x / 2 + Second.transform.localScale.y / 2) / 2;
        Vector3 FirstSize = First.transform.localScale;
        Vector3 FirstPos = First.transform.position;

        List<Vector3> Points = new List<Vector3>();
        Points.Add(Second.transform.position + new Vector3(Size, Size, 0));
        Points.Add(Second.transform.position + new Vector3(Size, -Size, 0));
        Points.Add(Second.transform.position + new Vector3(-Size, Size, 0));
        Points.Add(Second.transform.position + new Vector3(-Size, -Size, 0));

        foreach (Vector3 Point in Points)
        {
            Debug.DrawLine(new Vector3(Point.x, Point.y, 1), new Vector3(Second.transform.position.x, Second.transform.position.y, 1), Color.red);
            if (!(Point.x >= FirstPos.x - FirstSize.x / 2 && Point.x <= FirstPos.x + FirstSize.x / 2 && Point.y >= FirstPos.y - FirstSize.y / 2 && Point.y <= FirstPos.y + FirstSize.y / 2)) return false;
        }
        return true;
    }

    bool IsTouchingTower(Transform First, Transform Second) //guh
    {
        Vector3 Pos1 = First.position;
        Vector3 Pos2 = Second.position;

        float Distance = (Second.localScale.x / 2 + Second.localScale.y / 2) / 2;

        return (Pos2 - Pos1).magnitude < Distance;
    }
}

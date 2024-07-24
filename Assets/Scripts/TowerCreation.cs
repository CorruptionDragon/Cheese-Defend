using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerCreation : MonoBehaviour
{
    GameObject TowersHolder = null;

    GameObject PlacementObject = null;
    GameObject CurrentPlacement = null;

    bool CanPlace = false;

    private void Start()
    {
        TowersHolder = GameObject.Find("TowersHolder"); //making sure someone doesn't do a stupid
        if (TowersHolder == null) Debug.LogWarning("No TowersHolder was found. Insert an empty into the scene and rename it to TowersHolder");
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
            void SetUnplace()
            {
                found = true;
                CanPlace = false;
                PlacementObject.GetComponent<SpriteRenderer>().color = new Color(255, 0, 0, 0.75f);
            }
            for (int i = 0; i < TowersHolder.transform.childCount; i++)
            { 
                Transform Child = TowersHolder.transform.GetChild(i);
                if (IsTouchingTower(PlacementObject.transform, Child) == true)
                {
                    SetUnplace();
                    break;
                }
            }
            foreach (GameObject Child in GameObject.FindGameObjectsWithTag("NoTowerPlacement"))
            {
                if (found == true) break;

                if (IsTouchingObject(PlacementObject, Child) == true)
                {
                    SetUnplace();
                    break;
                }
            }
            if (found == false) { 
                CanPlace = true; 
                PlacementObject.GetComponent<SpriteRenderer>().color = new Color(0, 255, 0, 0.75f); 
            }

            if (Input.GetMouseButtonDown(0) && CanPlace)
            {
                PlaceSelected(playerMousePosition);
                return;
            }

            PlacementObject.transform.position = playerMousePosition;
        };
    }

    void PlaceSelected(Vector2 playerMousePosition)
    {
        GameObject NewTower = Instantiate(CurrentPlacement, playerMousePosition, Quaternion.identity);
        NewTower.name = "Tower";
        NewTower.transform.parent = TowersHolder.transform;

        CurrentPlacement = null;
        Destroy(PlacementObject);
    }

    public void StartPlacement(GameObject Tower)
    {
        if (PlacementObject != null) {
            CurrentPlacement = null;
            Destroy(PlacementObject);
        };

        CanPlace = false;
        CurrentPlacement = Tower;

        Vector2 playerMousePosition = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
        PlacementObject = Instantiate(CurrentPlacement, playerMousePosition, Quaternion.identity);
        PlacementObject.name = "TowerPlacement";
        PlacementObject.GetComponent<SpriteRenderer>().color = new Color(0, 255, 0, 0.75f);
    }

    bool IsTouchingObject(GameObject First, GameObject Second) //no idea how well this works
    {
        if (First.GetComponent<SpriteRenderer>().bounds.Intersects(Second.GetComponent<SpriteRenderer>().bounds)) return true;
        return false;
    }

    bool IsTouchingTower(Transform First, Transform Second) //guh
    {
        Vector3 Pos1 = First.position;
        Vector3 Pos2 = Second.position;

        float Distance = Second.localScale.x / 2 + Second.localScale.y / 2;

        return (Pos2 - Pos1).magnitude < Distance;
    }
}

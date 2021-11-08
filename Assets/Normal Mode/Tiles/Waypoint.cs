using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Waypoint : MonoBehaviour
{
    [SerializeField] Tower towerPrefab;
    [SerializeField] bool isPlaceable;
    
    public bool IsPlaceable { get { return isPlaceable; } }

    private void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (isPlaceable)
            {
                bool isSuccussful = towerPrefab.CreateTower(towerPrefab, transform.position);
                if (isSuccussful)
                {
                    isPlaceable = false;
                }
            }
        }
    }

    public void SetSelectedTower(Tower tower)
    {
        towerPrefab = tower;
    }
}

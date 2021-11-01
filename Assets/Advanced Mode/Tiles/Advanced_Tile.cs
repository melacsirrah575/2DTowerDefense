using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Advanced_Tile : MonoBehaviour
{
    [SerializeField] Tower towerPrefab;

    [SerializeField] bool isPlaceable;
    public bool IsPlaceable { get { return isPlaceable; } }

    Advanced_GridManager gridManager;
    Advanced_Pathfinder pathfinder;
    Vector2Int coordinates = new Vector2Int();

    private void Awake()
    {
        gridManager = FindObjectOfType<Advanced_GridManager>();
        pathfinder = FindObjectOfType<Advanced_Pathfinder>();
    }

    private void Start()
    {
        //Using != instead of == null because I don't want to quit out of this early
        if(gridManager != null)
        {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);

            if (!isPlaceable)
            {
                gridManager.BlockNode(coordinates);
            }
        }
    }

    private void OnMouseDown()
    {
        if (gridManager.GetNode(coordinates).isWalkable && !pathfinder.WillBlockPath(coordinates))
        {
            bool isSuccussful = towerPrefab.CreateTower(towerPrefab, transform.position);
            if (isSuccussful)
            {
                gridManager.BlockNode(coordinates);
                pathfinder.NotifyReceivers();
            }
        }
    }
}

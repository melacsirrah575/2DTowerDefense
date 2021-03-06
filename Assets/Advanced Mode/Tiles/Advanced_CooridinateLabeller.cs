using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

[ExecuteAlways]
[RequireComponent(typeof(TextMeshPro))]
public class Advanced_CooridinateLabeller : MonoBehaviour
{
    [SerializeField] Color defaultColor = Color.white;
    [SerializeField] Color blockedColor = Color.gray;
    [SerializeField] Color exploredColor = Color.yellow;
    [SerializeField] Color pathColor = new Color(1f, 0.5f, 0f); //Orange color

    TextMeshPro label;
    Vector2Int coordinates = new Vector2Int();
    Advanced_GridManager gridManager;


    void Awake()
    {
        gridManager = FindObjectOfType<Advanced_GridManager>();
        label = GetComponent<TextMeshPro>();
        label.enabled = false;
        DisplayCoordinates();
    }

    // Update is called once per frame
    void Update()
    {
        if(!Application.isPlaying)
        {
            DisplayCoordinates();
            UpdateObjectName();
            //Line below allows you to see Coords in editor
            label.enabled = true;
        }

        SetLabelColor();
        ToggleLabels();
    }

    private void SetLabelColor()
    {
        //Return early if gridManager is null
        if(gridManager == null) { return; }

        Advanced_Node node = gridManager.GetNode(coordinates);

        //Return early if node is null
        if(node == null) { return; }

        //Coloring labels based on Node bools
        if(!node.isWalkable)
        {
            label.color = blockedColor;
        } else if (node.isPath)
        {
            label.color = pathColor;
        } else if (node.isExplored)
        {
            label.color = exploredColor;
        } else
        {
            label.color = defaultColor;
        }
    }

    void ToggleLabels()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            label.enabled = !label.IsActive();
        }
    }

    private void DisplayCoordinates()
    {
        if(gridManager == null) { return; }

        //Displays Coordinates
        coordinates.x = Mathf.RoundToInt(transform.position.x / gridManager.UnityGridSize);
        coordinates.y = Mathf.RoundToInt(transform.position.y / gridManager.UnityGridSize);
        label.text = coordinates.x + "," + coordinates.y;
    }

    void UpdateObjectName() //Updates TileName based on TilePosition
    {
        transform.parent.name = coordinates.ToString();
    }
}

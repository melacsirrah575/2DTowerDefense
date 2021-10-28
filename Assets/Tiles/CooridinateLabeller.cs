using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

[ExecuteAlways]
[RequireComponent(typeof(TextMeshPro))]
public class CooridinateLabeller : MonoBehaviour
{
    [SerializeField] Color defaultColor = Color.white;
    [SerializeField] Color blockedColor = Color.gray;
    [SerializeField] Color exploredColor = Color.yellow;
    [SerializeField] Color pathColor = new Color(1f, 0.5f, 0f);

    TextMeshPro label;
    Vector2Int coordinates = new Vector2Int();
    GridManager gridManager;


    void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
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
        }

        SetLabelColor();
        ToggleLabels();
    }

    private void SetLabelColor()
    {
        //Return early if gridManager is null
        if(gridManager == null) { return; }

        Node node = gridManager.GetNode(coordinates);

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
        //Displays Coordinates as 1,0 2,0 etc. Instead of however Snap Settings are configured
        coordinates.x = Mathf.RoundToInt(transform.position.x / UnityEditor.EditorSnapSettings.move.x);
        coordinates.y = Mathf.RoundToInt(transform.position.y / UnityEditor.EditorSnapSettings.move.y);
        label.text = coordinates.x + "," + coordinates.y;
    }

    void UpdateObjectName() //Updates TileName based on TilePosition
    {
        transform.parent.name = coordinates.ToString();
    }
}

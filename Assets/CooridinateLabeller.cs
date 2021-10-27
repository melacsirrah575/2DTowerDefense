using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

[ExecuteAlways]
public class CooridinateLabeller : MonoBehaviour
{
    TextMeshPro label;
    Vector2Int coordinates = new Vector2Int();

    void Awake()
    {
        label = GetComponent<TextMeshPro>();
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

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Advanced_GridManager : MonoBehaviour
{
    [SerializeField] Vector2Int gridSize;
    [Tooltip("World Grid Size - Should match UnityEditor snap settings")]
    [SerializeField] int unityGridSize = 1;
    public int UnityGridSize { get { return unityGridSize; } }
    Dictionary<Vector2Int, Advanced_Node> grid = new Dictionary<Vector2Int, Advanced_Node>();

    //Creating Grid property
    public Dictionary<Vector2Int, Advanced_Node> Grid { get { return grid; } }

    private void Awake()
    {
        CreateGrid();
    }

    public Advanced_Node GetNode(Vector2Int coordinates)
    {
        if (grid.ContainsKey(coordinates))
        {
            return grid[coordinates];
        }

        return null;
    }

    public void BlockNode(Vector2Int coordinates)
    {
        if (grid.ContainsKey(coordinates))
        {
            grid[coordinates].isWalkable = false;
        }
    }

    public void ResetNodes()
    {
        foreach(KeyValuePair<Vector2Int, Advanced_Node> entry in grid)
        {
            entry.Value.connectedTo = null;
            entry.Value.isExplored = false;
            entry.Value.isPath = false;
        }
    }

    //Allows us to get Coords from a Position
    public Vector2Int GetCoordinatesFromPosition(Vector3 position)
    {
        Vector2Int coordinates = new Vector2Int();
        coordinates.x = Mathf.RoundToInt(position.x / unityGridSize);
        coordinates.y = Mathf.RoundToInt(position.y / unityGridSize);

        return coordinates;
    }

    //Allows us to get a Position from a set of Coords
    public Vector2 GetPositionFromCoordinates(Vector2Int coordinates)
    {
        Vector2 position = new Vector2();
        position.x = coordinates.x * unityGridSize;
        position.y = coordinates.y * unityGridSize;

        return position;
    }

    private void CreateGrid()
    {
        //Creating nested for loop to create grid
        for(int x = 0; x < gridSize.x; x++)
        {
            for(int y = 0; y < gridSize.y; y++)
            {
                Vector2Int coordinates = new Vector2Int(x, y);
                grid.Add(coordinates, new Advanced_Node(coordinates, true));
                //Line below displays every coordinate in grid when uncommented
                //Debug.Log(grid[coordinates].coordinates + " = " + grid[coordinates].isWalkable);
            }
        }
    }
}

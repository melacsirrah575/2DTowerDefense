using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Advanced_Pathfinder : MonoBehaviour
{
    [SerializeField] Vector2Int startCoordinates;
    public Vector2Int StartCoordinates { get { return startCoordinates; } }
    [SerializeField] Vector2Int destinationCoordinates;
    public Vector2Int DestinationCoordinates { get { return destinationCoordinates; } }


    Advanced_Node startNode;
    Advanced_Node destinationNode;
    Advanced_Node currentSearchNode;

    Dictionary<Vector2Int, Advanced_Node> reached = new Dictionary<Vector2Int, Advanced_Node>();
    //Using queue to enforce FIFO
    Queue<Advanced_Node> frontier = new Queue<Advanced_Node>();

    //Order of Directions in directions array will determine how pathfinding works
    Vector2Int[] directions = { Vector2Int.right, Vector2Int.left, Vector2Int.up, Vector2Int.down };
    Advanced_GridManager gridManager;
    Dictionary<Vector2Int, Advanced_Node> grid = new Dictionary<Vector2Int, Advanced_Node>();

    private void Awake()
    {
        gridManager = FindObjectOfType<Advanced_GridManager>();

        if(gridManager != null)
        {
            grid = gridManager.Grid;
            //Gets Start and end point from gridManager
            startNode = grid[startCoordinates];
            destinationNode = grid[destinationCoordinates];
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        GetNewPath();
    }

    public List<Advanced_Node> GetNewPath()
    {
        return GetNewPath(startCoordinates);
    }

    //Overloading GetNewPath to take a Vector2Int for BreadthFirstSearch
    public List<Advanced_Node> GetNewPath(Vector2Int coordinates)
    {
        gridManager.ResetNodes();
        BreadthFirstSearch(coordinates);
        return BuildPath();
    }

    //Explores neighbors based on directions
    private void ExploreNeighbors()
    {
        List<Advanced_Node> neighbors = new List<Advanced_Node>();

        foreach(Vector2Int direction in directions)
        {
            Vector2Int neighborCoords = currentSearchNode.coordinates + direction;

            if (grid.ContainsKey(neighborCoords))
            {
                neighbors.Add(grid[neighborCoords]);

            }
        }

        foreach(Advanced_Node neighbor in neighbors)
        {
            if(!reached.ContainsKey(neighbor.coordinates) && neighbor.isWalkable)
            {
                neighbor.connectedTo = currentSearchNode;
                reached.Add(neighbor.coordinates, neighbor);
                frontier.Enqueue(neighbor);
            }
        }
    }

    //Implementing BreadthFirstSearch pathfinding
    void BreadthFirstSearch(Vector2Int coordinates)
    {
        startNode.isWalkable = true;
        destinationNode.isWalkable = true;

        frontier.Clear();
        reached.Clear();

        bool isRunning = true;

        frontier.Enqueue(grid[coordinates]);
        reached.Add(coordinates, grid[coordinates]);

        while(frontier.Count > 0 && isRunning) 
        {
            currentSearchNode = frontier.Dequeue();
            currentSearchNode.isExplored = true;
            ExploreNeighbors();
            if(currentSearchNode.coordinates == destinationCoordinates)
            {
                isRunning = false;
            }
        }
    }

    //Getting the list of Nodes that will make up the path
    List<Advanced_Node> BuildPath()
    {
        List<Advanced_Node> path = new List<Advanced_Node>();
        Advanced_Node currentNode = destinationNode;

        path.Add(currentNode);
        currentNode.isPath = true;

        while(currentNode.connectedTo != null)
        {
            currentNode = currentNode.connectedTo;
            path.Add(currentNode);
            currentNode.isPath = true;
        }

        path.Reverse();
        return path;
    }

    //Checks to see if BreadthFirstSearch can find a path through nodes if this one were to be blocked
    public bool WillBlockPath(Vector2Int coordinates)
    {
        if (grid.ContainsKey(coordinates))
        {
            bool previousState = grid[coordinates].isWalkable;

            grid[coordinates].isWalkable = false;
            List<Advanced_Node> newPath = GetNewPath();
            grid[coordinates].isWalkable = previousState;

            if(newPath.Count <= 1)
            {
                GetNewPath();
                return true;
            }
        }

        return false;
    }

    //Tells Enemy to run RecalculatePath function. Sends whether or not there are any recievers
    public void NotifyReceivers()
    {
        BroadcastMessage("RecalculatePath", false ,SendMessageOptions.DontRequireReceiver);
    }

}

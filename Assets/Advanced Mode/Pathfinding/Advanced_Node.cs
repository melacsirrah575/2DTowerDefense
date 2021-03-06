using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Advanced_Node
{
    public Vector2Int coordinates;
    public bool isWalkable;
    public bool isExplored;
    public bool isPath;
    public Advanced_Node connectedTo;

    public Advanced_Node(Vector2Int coordinates, bool isWalkable)
    {
        //This.variable is the public vector above. They are being equal to the ones in this Node Class
        this.coordinates = coordinates;
        this.isWalkable = isWalkable;
    }
}

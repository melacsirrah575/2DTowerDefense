using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Node
{
    public Vector2Int coordinates;
    public bool isWalkable;
    public bool isExplored;
    public bool isPath;
    public Node connectedTo;

    public Node(Vector2Int m_coordinates, bool m_isWalkable)
    {
        coordinates = m_coordinates;
        isWalkable = m_isWalkable;
    }
}

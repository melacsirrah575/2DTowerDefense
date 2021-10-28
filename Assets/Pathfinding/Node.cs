using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Node
{
    public Vector2Int m_coordinates;
    public bool m_isWalkable;
    public bool m_isExplored;
    public bool m_isPath;
    public Node m_connectedTo;

    public Node(Vector2Int coordinates, bool isWalkable)
    {
        coordinates = m_coordinates;
        isWalkable = m_isWalkable;
    }
}

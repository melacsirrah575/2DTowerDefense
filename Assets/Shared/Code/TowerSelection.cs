using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSelection : MonoBehaviour
{
    [SerializeField] private Tower tower;

    private GameObject[] tiles;

    // Start is called before the first frame update
    void Start()
    {
        tiles = GameObject.FindGameObjectsWithTag("Tile");
    }

    public void SetSelectedTower()
    {
        foreach (GameObject tile in tiles)
        {
            if (tile.TryGetComponent<Waypoint>(out Waypoint waypoint))
            {
                waypoint.SetSelectedTower(tower);
            }
            else if (tile.TryGetComponent<Advanced_Tile>(out Advanced_Tile adTile))
            {
                adTile.SetSelectedTower(tower);
            }
        }
    }
}

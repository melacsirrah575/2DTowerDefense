using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] [Range(0f, 5f)] float speed = 1f;

    List<Node> path = new List<Node>();

    Enemy enemy;
    Pathfinder pathfinder;
    GridManager gridManager;

    void OnEnable()
    {
        ReturnToStart();
        //Recalculate Path set to true because we want it to happen when re-enabled at start
        RecalculatePath(true);
    }

    private void Awake()
    {
        pathfinder = FindObjectOfType<Pathfinder>();
        gridManager = FindObjectOfType<GridManager>();
        enemy = GetComponent<Enemy>();
    }

    void RecalculatePath(bool resetPath)
    {
        //Clears any potential found path before finding path again
        Vector2Int coordinates = new Vector2Int();

        if(resetPath)
        {
            coordinates = pathfinder.StartCoordinates;
        } else
        {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);
        }
        StopAllCoroutines();
        path.Clear();
        path = pathfinder.GetNewPath(coordinates);
        StartCoroutine(FollowPath());
    }

    //Places enemy at start of path
    void ReturnToStart()
    {
        transform.position = gridManager.GetPositionFromCoordinates(pathfinder.StartCoordinates);
    }

    IEnumerator FollowPath() //Goes through the path on 1 second delay for each waypoint
    {
        //setting i = 1 prevents enemies from idling when instantiated and also fixes unintended, odd movement on path update
        for(int i = 1; i < path.Count; i++)
        {
            Vector3 startPosition = transform.position;
            Vector3 endPosition = gridManager.GetPositionFromCoordinates(path[i].coordinates);
            float travelPercent = 0f;

            //Makes sure enemy faces direction they are heading
            Vector3 dir = endPosition - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            while(travelPercent < 1f) //Creating smooth movement using LERP
            {
                travelPercent += Time.deltaTime * speed;
                transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
                yield return new WaitForEndOfFrame();
            }
        }
        FinishPath();
    }

    private void FinishPath()
    {
        //Steal Gold and/or lose life happens here!
        gameObject.SetActive(false);
    }
}

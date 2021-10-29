using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] List<Waypoint> path = new List<Waypoint>();
    [SerializeField] [Range(0f, 5f)] float speed = 1f;

    Enemy enemy;
    int currentWaypoint = 0;

    void OnEnable()
    {
        FindPath();
        ReturnToStart();
        StartCoroutine(FollowPath());
    }

    private void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    void FindPath()
    {
        //Clears any potential found path before finding path again
        path.Clear();

        //Takes Parent Object with Path Tag and makes a Path with children
        //Makes path starting with first child and moves down from there
        GameObject parent = GameObject.FindGameObjectWithTag("Path");
        foreach(Transform child in parent.transform)
        {
            Waypoint waypoint = child.GetComponent<Waypoint>();

            if(waypoint != null)
            {
                path.Add(waypoint);
            }
        }
    }

    //Places enemy at start of path
    void ReturnToStart()
    {
        transform.position = path[0].transform.position;
    }

    IEnumerator FollowPath() //Goes through the path on 1 second delay for each waypoint
    {
        foreach(Waypoint waypoint in path)
        {
            currentWaypoint++;

            Vector3 startPosition = transform.position;
            Vector3 endPosition = waypoint.transform.position;
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
        currentWaypoint = 0;
    }

    public int GetCurrentWaypoint()
    {
        return currentWaypoint;
    }
}

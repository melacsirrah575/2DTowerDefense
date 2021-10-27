using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] List<Waypoint> path = new List<Waypoint>();
    [SerializeField] float waitTime = 1f;

    void Start()
    {
        Debug.Log("Path Starts here");
        StartCoroutine(FollowPath());
        Debug.Log("Path Ends here");

    }

    IEnumerator FollowPath() //Goes through the path on 1 second delay for each waypoint
    {
        foreach(Waypoint waypoint in path)
        {
            transform.position = waypoint.transform.position;
            yield return new WaitForSeconds(waitTime);
        }
    }
}

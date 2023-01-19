using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{

    [SerializeField] List<Waypoint> path = new List<Waypoint>();
    [Tooltip("The time in seconds to move to the next tile along the path.")]
    [SerializeField] float waitTime = 1f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FollowPath());
    }

    // Coroutine to move along the path
    IEnumerator FollowPath() {
        foreach (Waypoint wp in path) {
            transform.position = wp.transform.position;
            yield return new WaitForSeconds(waitTime);
        }
    }
}

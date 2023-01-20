using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{

    [SerializeField] List<Waypoint> path = new List<Waypoint>();
    [SerializeField] [Range(0f, 10f)] float speed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FollowPath());
    }

    // Coroutine to move along the path
    IEnumerator FollowPath() {
        foreach (Waypoint wp in path) {
            Vector3 startPos = transform.position;
            Vector3 endPos = wp.transform.position;
            float travelPercent = 0f;

            transform.LookAt(endPos);

            while(travelPercent < 1f) {
                transform.position = Vector3.Lerp(startPos, endPos, travelPercent);
                travelPercent += speed * Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
        }
    }
}

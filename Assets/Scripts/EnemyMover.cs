using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{

    [SerializeField] List<Waypoint> path = new List<Waypoint>();
    [SerializeField] [Range(0f, 10f)] float speed = 1f;

    Enemy enemy;

    private static string pathTag = "Path";

    void Start() {
        enemy = GetComponent<Enemy>();
    }

    // Start is called before the first frame update
    void OnEnable() {
        FindPath(); // TODO - move FindPath to Start?
        MoveToPathStart();
        StartCoroutine(FollowPath());
    }

    void MoveToPathStart() {
        transform.position = path[0].transform.position;
        path.Remove(path[0]);
    }

    void FindPath() {
        path.Clear();

        // get path parents (tiles are in order within)
        GameObject pathParentObject = GameObject.FindGameObjectWithTag(pathTag);
        foreach (Waypoint wp in pathParentObject.GetComponentsInChildren<Waypoint>()) {
            if (null != wp) {
                path.Add(wp);
            }
        }
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

        // reached end of path
        Attack();
    }

    void Attack() {
        gameObject.SetActive(false);
        enemy.IncurPenalty();
    }
}

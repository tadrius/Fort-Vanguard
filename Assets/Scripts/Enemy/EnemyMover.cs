using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{

    [SerializeField] List<Tile> path = new List<Tile>();
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

        // get path object (path tiles are children in order)
        GameObject pathObject = GameObject.FindGameObjectWithTag(pathTag);
        foreach (Tile tile in pathObject.GetComponentsInChildren<Tile>()) {
            if (null != tile) {
                path.Add(tile);
            }
        }
    }

    // Coroutine to move along the path
    IEnumerator FollowPath() {
        foreach (Tile tile in path) {
            Vector3 startPos = transform.position;
            Vector3 endPos = tile.transform.position;
            float travelPercent = 0f;

            transform.LookAt(endPos);

            while(travelPercent < 1f) {
                transform.position = Vector3.Lerp(startPos, endPos, travelPercent);
                travelPercent += speed * Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
        }

        // reached end of path
        FinishPath();
    }

    void FinishPath() {
        gameObject.SetActive(false);
        enemy.IncurPenalty();
    }
}

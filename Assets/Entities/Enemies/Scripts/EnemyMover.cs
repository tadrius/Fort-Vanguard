using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{

    [SerializeField] [Range(0f, 10f)] float speed = 1f;

    Enemy enemy;
    Pathfinder pathfinder;
    GridManager gridManager;
    List<Node> path = new List<Node>();

    void Awake() {
        enemy = GetComponent<Enemy>();
        pathfinder = FindObjectOfType<Pathfinder>();
        gridManager = FindObjectOfType<GridManager>();
    }

    public void BeginMoving() {
        enemy.PlayWalkAnimations(speed);
        MoveToPathStart();
        FindPath(true);
    }

    void MoveToPathStart() {
        transform.position = gridManager.GetPositionFromCoordinates(pathfinder.StartCoordinates);
    }

    readonly public static string findPathMethodName = "FindPath";
    void FindPath(bool resetPath) {
        Vector2Int startCoordinates, endCoordinates;
        if (resetPath) {
            startCoordinates = pathfinder.StartCoordinates;
            endCoordinates = pathfinder.EndCoordinates;
        } else {
            startCoordinates = gridManager.GetCoordinatesFromPosition(transform.position);
            endCoordinates = pathfinder.EndCoordinates;
        }
        StopAllCoroutines();

        path.Clear();
        path = pathfinder.FindPath(startCoordinates, endCoordinates);

        StartCoroutine(FollowPath());
    }

    // Coroutine to move along the path
    IEnumerator FollowPath() {
        for (int i = 1; i < path.Count; i++) { // skip the starting node (enemy will already be there)
            Vector3 startPos = transform.position;
            Vector3 endPos = gridManager.GetPositionFromCoordinates(path[i].coordinates);
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
        enemy.SpawnPenaltyFX();
        enemy.IncurPenalty();
        gameObject.SetActive(false);
    }
}

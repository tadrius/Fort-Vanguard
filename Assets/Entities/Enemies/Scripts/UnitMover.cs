using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMover : MonoBehaviour
{

    float speed = 1f;

    Unit unit;
    Pathfinder pathfinder;
    GridManager gridManager;
    List<Node> path = new List<Node>();

    void Awake() {
        unit = transform.parent.GetComponent<Unit>();
        pathfinder = FindObjectOfType<Pathfinder>();
        gridManager = FindObjectOfType<GridManager>();
    }

    public void BeginMoving(float speed) {
        this.speed = speed;
        unit.PlayWalkAnimation(speed);
        MoveToPathStart();
        FindPath(true);
    }

    void MoveToPathStart() {
        unit.transform.position = gridManager.GetPositionFromCoordinates(pathfinder.StartCoordinates);
    }

    readonly public static string findPathMethodName = "FindPath";
    void FindPath(bool resetPath) {
        Vector2Int startCoordinates, endCoordinates;
        if (resetPath) {
            startCoordinates = pathfinder.StartCoordinates;
            endCoordinates = pathfinder.EndCoordinates;
        } else {
            startCoordinates = gridManager.GetCoordinatesFromPosition(unit.transform.position);
            endCoordinates = pathfinder.EndCoordinates;
        }
        StopAllCoroutines();

        path.Clear();
        path = pathfinder.FindPath(startCoordinates, endCoordinates);

        StartCoroutine(FollowPath());
    }

    // Coroutine to move along the path
    IEnumerator FollowPath() {
        for (int i = 1; i < path.Count; i++) { // skip the starting node
            Vector3 startPos = unit.transform.position;
            Vector3 endPos = gridManager.GetPositionFromCoordinates(path[i].coordinates);
            float travelPercent = 0f;

            unit.transform.LookAt(endPos);

            while(travelPercent < 1f) {
                unit.transform.position = Vector3.Lerp(startPos, endPos, travelPercent);
                travelPercent += speed * Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
        }

        // reached end of path
        FinishPath();
    }

    void FinishPath() {
        unit.SpawnPenaltyFX();
        unit.ReduceWaveUnitCount();
        unit.IncurPenalty();
        unit.gameObject.SetActive(false);
        transform.parent.gameObject.SetActive(false);
    }
}

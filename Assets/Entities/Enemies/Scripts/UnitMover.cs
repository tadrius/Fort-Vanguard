using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Unit))]
public class UnitMover : MonoBehaviour
{

    [SerializeField] [Range(0f, 10f)] float speed = 1f;

    Unit unit;
    Pathfinder pathfinder;
    GridManager gridManager;
    List<Node> path = new List<Node>();

    Transform entity; // manipulate the entity's transform to move the entire entity, including the mesh and FX

    void Awake() {
        entity = transform.parent;
        unit = GetComponent<Unit>();
        pathfinder = FindObjectOfType<Pathfinder>();
        gridManager = FindObjectOfType<GridManager>();
    }

    public void BeginMoving() {
        unit.PlayWalkAnimation(speed);
        MoveToPathStart();
        FindPath(true);
    }

    void MoveToPathStart() {
        entity.position = gridManager.GetPositionFromCoordinates(pathfinder.StartCoordinates);
    }

    readonly public static string findPathMethodName = "FindPath";
    void FindPath(bool resetPath) {
        Vector2Int startCoordinates, endCoordinates;
        if (resetPath) {
            startCoordinates = pathfinder.StartCoordinates;
            endCoordinates = pathfinder.EndCoordinates;
        } else {
            startCoordinates = gridManager.GetCoordinatesFromPosition(entity.position);
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
            Vector3 startPos = entity.position;
            Vector3 endPos = gridManager.GetPositionFromCoordinates(path[i].coordinates);
            float travelPercent = 0f;

            entity.LookAt(endPos);

            while(travelPercent < 1f) {
                entity.position = Vector3.Lerp(startPos, endPos, travelPercent);
                travelPercent += speed * Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
        }

        // reached end of path
        FinishPath();
    }

    void FinishPath() {
        unit.SpawnPenaltyFX();
        unit.IncurPenalty();
        unit.gameObject.SetActive(false);
        transform.parent.gameObject.SetActive(false);
    }
}

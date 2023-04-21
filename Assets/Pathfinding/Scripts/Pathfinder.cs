using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{

    [SerializeField] Vector2Int startCoordinates;
    public Vector2Int StartCoordinates { get { return startCoordinates; }}
    [SerializeField] Vector2Int endCoordinates;
    public Vector2Int EndCoordinates { get { return endCoordinates; }}
    Node startNode;
    Node endNode;
    Node currentNode;

    Queue<Node> toSearch = new Queue<Node>();
    Dictionary<Vector2Int, Node> traversed = // to track of what nodes have already been traversed
        new Dictionary<Vector2Int, Node>();

    Vector2Int[] directions = // order of directions changes order of search algorithm
        { Vector2Int.right, Vector2Int.left, Vector2Int.up, Vector2Int.down };
    GridManager gridManager;
    Dictionary<Vector2Int, Node> grid;

    void Awake() {
        gridManager = FindObjectOfType<GridManager>();
        if (null != gridManager) {
            grid = gridManager.Grid;
            startNode = grid[startCoordinates];
            startNode.isTraversable = true;
            endNode = grid[endCoordinates];
            endNode.isTraversable = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        FindPath();
    }

    public List<Node> FindPath() {
        return FindPath(startCoordinates, endCoordinates);
    }

    public List<Node> FindPath(Vector2Int startCoordinates, Vector2Int endCoordinates) {
        gridManager.ResetNodes();
        BreadthFirstSearch(startCoordinates, endCoordinates);
        return BuildPath();
    }

    void ExploreNeighbors() {
        List<Node> neighbors = new List<Node>();

        // find neighbors of the current node 
        foreach(Vector2Int dir in directions) {
            Node neighbor = gridManager.GetNode(currentNode.coordinates + dir);
            if (null != neighbor) {
                neighbors.Add(neighbor);
            }
        }

        // for each untraversed neighbor, mark as traversed and add to the search queue
        foreach (Node neighbor in neighbors) {
            if (neighbor.isTraversable && !traversed.ContainsKey(neighbor.coordinates)) {
                neighbor.parent = currentNode;
                traversed.Add(neighbor.coordinates, neighbor);
                toSearch.Enqueue(neighbor);
            }
        }
    }

    void BreadthFirstSearch(Vector2Int startCoordinates, Vector2Int endCoordinates) {
        // reset
        toSearch.Clear();
        traversed.Clear();

        // setup
        Node startNode = gridManager.GetNode(startCoordinates);
        if (null == startNode) {
            Debug.Log($"No node available at {startCoordinates}");
            return;
        }
        toSearch.Enqueue(startNode);
        traversed.Add(startCoordinates, startNode);

        while (0 < toSearch.Count) {
            currentNode = toSearch.Dequeue();
            currentNode.isExplored = true;
            ExploreNeighbors();
            if (currentNode.coordinates.Equals(endCoordinates)) {
                break;
            }
        }
    }

    List<Node> BuildPath() {
        List<Node> path = new List<Node>();
        Node pathNode = endNode;
        while (null != pathNode) {
            path.Add(pathNode);
            pathNode.isPath = true;            
            pathNode = pathNode.parent;
        }
        path.Reverse();
        return path;
    }

    // checks if blocking the node at the given coordinates will block the last possible path
    public bool WillBlockPath(Vector2Int coordinates) {
        if (grid.ContainsKey(coordinates)) {
            // temporarily change node state to check if the change would block possible paths 
            bool previousState = grid[coordinates].isTraversable;
            grid[coordinates].isTraversable = false;
            List<Node> path = FindPath();
            grid[coordinates].isTraversable = previousState;

            // if resulting path is too short, change would block route
            if (path.Count <= 1) {
                // Debug.Log("Blocking this node will block all possible paths.");
                FindPath();
                return true;
            }
        }
        return false;
    }

    public void NotifyReceivers() {
        BroadcastMessage(UnitMover.findPathMethodName,  // Broadcast message only reaches this GameObject and its children.
            false, SendMessageOptions.DontRequireReceiver);
    }

}

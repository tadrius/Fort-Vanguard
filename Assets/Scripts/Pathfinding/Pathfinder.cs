using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{

    [SerializeField] Vector2Int startCoordinates;
    [SerializeField] Vector2Int endCoordinates;
    Node startNode;
    Node endNode;
    Node currentNode;

    Queue<Node> toSearch = new Queue<Node>();
    Dictionary<Vector2Int, Node> traversed = // to track of what nodes have already been traversed
        new Dictionary<Vector2Int, Node>();

    Vector2Int[] directions = // order of directions changes order of search algorithm
        { Vector2Int.right, Vector2Int.left, Vector2Int.up, Vector2Int.down };
    GridManager gridManager;
    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();

    void Awake() {
        gridManager = FindObjectOfType<GridManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        startNode = gridManager.GetNode(startCoordinates);
        endNode = gridManager.GetNode(endCoordinates);
        startNode.isTraversable = true;
        endNode.isTraversable = true;
        BreadthFirstSearch();
        BuildPath();
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

    void BreadthFirstSearch() {
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

}

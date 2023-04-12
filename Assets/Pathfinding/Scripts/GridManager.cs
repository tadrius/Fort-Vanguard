using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{

    [Tooltip("Size of each side of a cell. Should match the Unity Editor Snap Settings.")]
    [SerializeField] float unityCellSize = 10f;
    [Tooltip("Number of tiles along the horizontal and vertical of the grid.")]
    [SerializeField] Vector2Int gridSize;
    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();

    public float UnityCellSize { get { return unityCellSize; }}
    public Dictionary<Vector2Int, Node> Grid { get { return grid; }}

    void Awake() {
        PopulateGrid();
    }

    void PopulateGrid() {
        for (int x = 0; x < gridSize.x; x++) {
            for (int y = 0; y < gridSize.y; y++) {
                Vector2Int coordinates = new Vector2Int(x, y);
                grid.Add(coordinates, new Node(coordinates, true)); // isTraversable true by default
            }
        }
    }

    public Node GetNode(Vector2Int coordinates) {
        if (grid.ContainsKey(coordinates)) {
            return grid[coordinates];
        }
        return null;
    }

    public void BlockNode(Vector2Int coordinates) {
        Node node = GetNode(coordinates);
        if (null != node) {
            node.isTraversable = false;
        }
    }

    public void UnblockNode(Vector2Int coordinates) {
        Node node = GetNode(coordinates);
        if (null != node) {
            node.isTraversable = true;
        }
    }

    public void ResetNodes() {
        foreach (KeyValuePair<Vector2Int, Node> entry in grid) {
            entry.Value.Reset();
        }
    }

    public Vector2Int GetCoordinatesFromPosition(Vector3 position) {
        Vector2Int coordinates = new Vector2Int();
        coordinates.x = Mathf.RoundToInt(position.x/unityCellSize);
        coordinates.y = Mathf.RoundToInt(position.z/unityCellSize); 
        return coordinates;
    }

    public Vector3 GetPositionFromCoordinates(Vector2Int coordinates) {
        Vector3 position = new Vector3();
        position.x = (float) coordinates.x * unityCellSize;
        position.y = 0f;
        position.z = (float) coordinates.y * unityCellSize;
        return position;
    }
}

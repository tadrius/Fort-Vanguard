using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteAlways]
[RequireComponent(typeof(TextMeshPro))]
public class CoordinateLabeler : MonoBehaviour
{
    
    [SerializeField] Color UntraversableColor = Color.grey;
    [SerializeField] Color exploredColor = Color.yellow;
    [SerializeField] Color pathColor = Color.magenta;
    [SerializeField] Color defaultColor = Color.white;


    TextMeshPro label;
    Vector2Int coordinates = new Vector2Int();
    GridManager gridManager;

    void Awake() {
        gridManager = FindObjectOfType<GridManager>();
        label = GetComponent<TextMeshPro>();
        label.enabled = false;
        UpdateCoordinatesLabel();
    }

    void Update()
    {
        // will run even when not playing
        if (!Application.isPlaying) {
            UpdateCoordinatesLabel();
            UpdateObjectName();
            label.enabled = true;
        }
        SetLabelColor();
        ToggleLabels();
    }

    void ToggleLabels() {
        if (Input.GetKeyDown(KeyCode.C)) {
            label.enabled = !label.IsActive();
        }
    }

    void SetLabelColor() {
        if (null == gridManager) {
            return;
        }
        Node node = gridManager.GetNode(coordinates);
        if (null != node) {
            if (!node.isTraversable) {
                label.color = UntraversableColor;
            } else if (node.isPath) {
                label.color = pathColor;
            } else if (node.isExplored) {
                label.color = exploredColor;
            } else {
                label.color = defaultColor;
            }
        }
    }

    void UpdateCoordinatesLabel() {
        coordinates.x = Mathf.RoundToInt(transform.parent.position.x/
            gridManager.UnityCellSize);
        coordinates.y = Mathf.RoundToInt(transform.parent.position.z/
            gridManager.UnityCellSize); // using 3d space z as grid y

        // construct coordinate label using string interpolation
        label.text = $"{coordinates.x},{coordinates.y}";
    }

    void UpdateObjectName() {
        transform.parent.name = coordinates.ToString();
    }
}

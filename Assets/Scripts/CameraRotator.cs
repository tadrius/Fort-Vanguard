using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotator : MonoBehaviour
{

    [SerializeField] float rotationSpeed = 60f;

    Vector3 focalPoint;

    void Awake() {
        focalPoint = new Vector3(transform.position.x, 0f, transform.position.x);
    }


    // Update is called once per frame
    void Update() {
        Vector3 rotationVector = new Vector3(rotationSpeed * Time.deltaTime, 0f, 0f);
        float rotation = rotationSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.Q)) {
            RotateClockwise(rotation);
        } else if (Input.GetKey(KeyCode.E)) {
            RotateCounterClockwise(rotation);
        }
    }

    public void RotateClockwise(float rotation) {
        RotateAroundFocalPoint(rotation);
    }

    public void RotateCounterClockwise(float rotation) {
        RotateAroundFocalPoint(-rotation);
    }

    void RotateAroundFocalPoint(float rotation) {
        transform.RotateAround(focalPoint, Vector3.up, rotation);
    }
}

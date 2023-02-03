using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{

    [SerializeField] float rotationSpeed = 25f;

    Vector3 focalPoint;

    void Awake() {
        focalPoint = new Vector3(transform.position.x, 0f, transform.position.x);
    }

    // Update is called once per frame
    void Update() {
        Vector3 rotationVector = new Vector3(rotationSpeed * Time.deltaTime, 0f, 0f);
        if (Input.GetKey(KeyCode.Q)) {
            RotateAroundFocalPoint(rotationSpeed * Time.deltaTime);
        } else if (Input.GetKey(KeyCode.E)) {
            RotateAroundFocalPoint(-rotationSpeed * Time.deltaTime);
        }
    }

    void RotateAroundFocalPoint(float rotation) {
        transform.RotateAround(focalPoint, Vector3.up, rotation);
    }
}

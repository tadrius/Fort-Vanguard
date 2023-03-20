using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartSelector : MonoBehaviour
{

    [SerializeField] List<GameObject> possibleParts; 


    public GameObject CreateRandomPart() {
        GameObject part = possibleParts[Mathf.RoundToInt(Random.Range(0f, (float) possibleParts.Count - 1))];
        return CreatePart(part);
    }

    public GameObject CreatePart(GameObject part) {
        transform.GetChild(0).gameObject.SetActive(false); // deactivate default mesh child
        GameObject newPart = GameObject.Instantiate(part, transform.position, transform.rotation, transform);
        return newPart;
    }

    public void SetPossibleParts(List<GameObject> partChoices) {
        possibleParts = partChoices;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartSelector : MonoBehaviour
{

    List<GameObject> possibleParts; 
    GameObject part;

    // create a random part from the possible parts list and return the index of the created part
    public int CreateRandomPart() {
        int index = Mathf.RoundToInt(Random.Range(0f, (float) possibleParts.Count - 1));
        return CreatePart(index);
    }

    public int CreateRandomPart(Material palette) {
        int index = CreateRandomPart();
        SetPartPalette(palette);
        return index;
    }

    // create the part from the possible parts list that corresponds to the given index
    public int CreatePart(int partIndex) {
        disableDefaultMesh();
        if (0 < possibleParts.Count && null != possibleParts[partIndex]) { // check for empty indices and empty list
            part = GameObject.Instantiate(possibleParts[partIndex], transform.position, transform.rotation, transform);
        }
        return partIndex;
    }

    public int CreatePart(int partIndex, Material palette) {
        CreatePart(partIndex);
        SetPartPalette(palette);
        return partIndex;
    }

    void disableDefaultMesh() {
        for (int i = 0; i < transform.childCount; i++) {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    public void SetPossibleParts(List<GameObject> partChoices) {
        possibleParts = partChoices;
    }

    public void SetPartPalette(Material palette) {
        if (null != part && null != palette) {
            part.GetComponentInChildren<MeshRenderer>().material = palette;
        }
    }

}

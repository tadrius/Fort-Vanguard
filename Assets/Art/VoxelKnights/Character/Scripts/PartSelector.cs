using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartSelector : MonoBehaviour
{

    List<GameObject> possibleParts; 
    GameObject part;

    public int CreateRandomPart(Material palette, bool overrideExisting) {
        int index = CreateRandomPart(overrideExisting);
        SetPartPalette(palette);
        return index;
    }

    public int CreateRandomPart(bool overrideExisting) {
        int index = Mathf.RoundToInt(Random.Range(0f, (float) possibleParts.Count - 1));
        return CreatePart(index, overrideExisting);
    }

    public int CreatePart(int partIndex, Material palette, bool overrideExisting) {
        CreatePart(partIndex, overrideExisting);
        SetPartPalette(palette);
        return partIndex;
    }

    public int CreatePart(int partIndex, bool overrideExisting) {
        if (null == part || overrideExisting) {
            if (overrideExisting) {
                DestroyExistingParts();
            }
            if (0 < possibleParts.Count && null != possibleParts[partIndex]) { // check for empty indices and empty list
                part = GameObject.Instantiate(possibleParts[partIndex], transform.position, transform.rotation, transform);
            }
        }
        return partIndex;
    }

    void DestroyExistingParts() {
        for (int i = 0; i < transform.childCount; i++) {
            DestroyImmediate(transform.GetChild(i).gameObject);
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

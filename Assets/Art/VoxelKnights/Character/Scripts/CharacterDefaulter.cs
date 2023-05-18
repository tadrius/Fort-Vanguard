using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDefaulter : MonoBehaviour
{

    // To easily create default parts for character prefab variants...
    // Open the variant, select all parts and revert (in case default were already set)
    // Exit the prefab and select an instance of the variant.
    // Reset the Character Defaulter component.
    // Apply changes to Prefab.
    void Reset() {
        GetComponent<CharacterGenerator>().CreateParts(false);
    }
}

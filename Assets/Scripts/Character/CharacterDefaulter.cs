using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDefaulter : MonoBehaviour
{
    void Reset() {
        GetComponent<CharacterGenerator>().CreateParts(false);
    }
}

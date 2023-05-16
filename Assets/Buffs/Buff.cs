using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff : MonoBehaviour
{

    [SerializeField] float reloadMultiplier = .75f;

    public float ReloadMultiplier { get { return reloadMultiplier; } }

}

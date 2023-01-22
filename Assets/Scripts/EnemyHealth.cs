using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    [SerializeField] int maxHitPoints = 10;
    int currentHitPoints = 0;

    void OnEnable()
    {
        currentHitPoints = maxHitPoints;  
    }

    void OnParticleCollision(GameObject other) {
        currentHitPoints--;
        if (0 >= currentHitPoints) {
            gameObject.SetActive(false);
        }
    }
}

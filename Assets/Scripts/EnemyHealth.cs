using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    [SerializeField] int maxHitPoints = 10;
    int currentHitPoints = 0;

    // Start is called before the first frame update
    void Start()
    {
        currentHitPoints = maxHitPoints;  
    }

    void OnParticleCollision(GameObject other) {
        currentHitPoints--;
        if (0 >= currentHitPoints) {
            Destroy(gameObject);
        }
    }
}

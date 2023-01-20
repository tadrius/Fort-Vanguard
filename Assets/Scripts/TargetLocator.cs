using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLocator : MonoBehaviour
{

    [SerializeField] Transform weapon;
    Transform target;

    // Start is called before the first frame update
    void Start()
    {
        EnemyMover enemy = FindObjectOfType<EnemyMover>();
        if (null != enemy) {
            target = enemy.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        AimWeapon();
    }

    void AimWeapon() {
        weapon.LookAt(target);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Buffer : MonoBehaviour
{

    [SerializeField] float range = 30f;
    [SerializeField] Buff buff;
    [SerializeField] List<Attacker> buffTargets = new List<Attacker>();
    [SerializeField] List<Buff> activeBuffs = new List<Buff>();

    Builder builder;
    CharacterAnimator animator;
    Building building;


    public Buff Buff { get { return buff; } }

    void Awake()
    {
        builder = FindObjectOfType<Builder>();
        animator = GetComponentInChildren<CharacterAnimator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        building = GetComponentInParent<Building>();
        if (building.IsElevated)
        {
            range *= 2;
        }
        UseSpecialAnimations();
    }

    private void Update()
    {
        FindBuffTargets();
        ApplyBuff();
    }

    void FindBuffTargets()
    {
        buffTargets = new List<Attacker>();
        foreach (Building building in builder.ExistingBuildings)
        {
            if (null != building && building.isActiveAndEnabled)
            {
                Attacker[] buildingAttackers = building.GetComponentsInChildren<Attacker>();
                foreach (Attacker attacker in buildingAttackers)
                {
                    if (InRange(attacker))
                    {
                        buffTargets.Add(attacker);
                    }
                }
            }
        }
    }

    public bool InRange(Attacker attacker)
    {
        float distance = Vector3.Distance(transform.position, attacker.transform.position);
        if (distance <= range)
        {
            return true;
        }
        return false;
    }

    void ApplyBuff()
    {
        foreach (Attacker attacker in buffTargets)
        {
            Buff buff = attacker.AddBuff(this);
            if (buff != null)
            {
                activeBuffs.Add(buff);
            }
        }
    }

    private void OnDestroy()
    {
        foreach (Buff activeBuff in activeBuffs)
        {
            if (null != activeBuff)
            {
                Destroy(activeBuff.gameObject);
            }
        }
        foreach (Attacker attacker in buffTargets)
        {
            attacker.BuffsChanged = true;
        }
    }

    void UseSpecialAnimations()
    {
        animator.UseSpecialAnimations();
        animator.SetAnimationDuration(0f);
        animator.SetAnimationLock(false);
    }

}

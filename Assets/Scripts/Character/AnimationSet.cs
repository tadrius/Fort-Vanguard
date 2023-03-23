using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSet : MonoBehaviour
{
    [SerializeField] List<CharacterAnimation> idleAnimations;
    [SerializeField] List<CharacterAnimation> walkAnimations;
    [SerializeField] List<CharacterAnimation> reloadAnimations;
    [SerializeField] List<CharacterAnimation> attackAnimations;
    [SerializeField] List<CharacterAnimation> deathAnimations;

    public List<CharacterAnimation> IdleAnimations { get { return idleAnimations; } }
    public List<CharacterAnimation> WalkAnimations { get { return walkAnimations; } }
    public List<CharacterAnimation> ReloadAnimations { get { return reloadAnimations; } }
    public List<CharacterAnimation> AttackAnimations { get { return attackAnimations; } }
    public List<CharacterAnimation> DeathAnimations { get { return deathAnimations; } }

}

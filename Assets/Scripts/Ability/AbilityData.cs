using UnityEngine;

[CreateAssetMenu(fileName = "New Ability", menuName = "Ability System/Ability")]
public class AbilityData : ScriptableObject
{
    public string abilityName;
    public GameObject vfx;
    public AudioClip sfx;
    public bool isMelee;
    public float damage;
    public GameObject projectilePrefab;
    public float cooldown;

    [Header("Animation Settings")]
    public AnimationClip animation;
    public string animationTrigger; // Name of the trigger parameter in Animator Controller

    [Header("Movement Settings")]
    public bool canMove; // 
    public float moveDelayDuration; // 
}

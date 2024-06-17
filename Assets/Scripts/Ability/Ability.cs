using System.Collections;
using UnityEngine;

public class Ability : MonoBehaviour
{
    public AbilityData abilityData;

    private Animator animator;
    private AudioSource audioSource;
    private bool isCooldown;
    private PlayerCharacterController playerCharacterController; // Reference to player controller

    private void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        playerCharacterController = GetComponent<PlayerCharacterController>();
        isCooldown = false;
    }

    public void UseAbility(AbilityType abilityType)
    {
        if (abilityData == null || isCooldown) return;

        StartCoroutine(CooldownRoutine());
        StartCoroutine(HandleAbility());

        // Trigger ability animation in Animator
        if (animator != null && !string.IsNullOrEmpty(abilityData.animationTrigger))
        {
            animator.SetTrigger(abilityData.animationTrigger);
        }
    }

    private IEnumerator HandleAbility()
    {
        // Disable movement if canMove is false
        if (playerCharacterController != null && !abilityData.canMove)
        {
            playerCharacterController.CanMove = false;
        }

        // Play VFX
        if (abilityData.vfx != null)
        {
            Instantiate(abilityData.vfx, transform.position, Quaternion.identity);
        }

        // Play SFX
        if (abilityData.sfx != null)
        {
            audioSource.PlayOneShot(abilityData.sfx);
        }

        // Handle melee attack
        if (abilityData.isMelee)
        {
            yield return HandleMeleeAttack();
        }

        // Handle projectile attack
        if (!abilityData.isMelee && abilityData.projectilePrefab != null)
        {
            FireProjectile();
        }

        // Wait for the move delay duration
        yield return new WaitForSeconds(abilityData.moveDelayDuration);

        // Re-enable movement
        if (playerCharacterController != null && !abilityData.canMove)
        {
            playerCharacterController.CanMove = true;
        }
    }

    private IEnumerator HandleMeleeAttack()
    {
        // Wait for the animation to reach the hit frame
        yield return new WaitForSeconds(abilityData.animation.length * 0.5f); // Adjust the timing if needed

        // Implement your damage logic here
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 2f);
        foreach (var hitCollider in hitColliders)
        {
            // Apply damage to the hit objects
            //hitCollider.GetComponent<Health>()?.TakeDamage(abilityData.damage);
        }
    }

    private void FireProjectile()
    {
        // Instantiate the projectile and set its direction
        GameObject projectile = Instantiate(abilityData.projectilePrefab, transform.position, Quaternion.identity);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = transform.forward * 10f; // Set projectile speed
        }
    }

    private IEnumerator CooldownRoutine()
    {
        isCooldown = true;
        yield return new WaitForSeconds(abilityData.cooldown);
        isCooldown = false;
    }
}

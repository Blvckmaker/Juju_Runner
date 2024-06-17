using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private AbilityManager abilityManager;
    [SerializeField] private CharacterTag currentTag; // Tag for the current character

    [SerializeField] private Button primaryAttackButton; // Assign this via inspector
    [SerializeField] private Button secondaryAttackButton; // Assign this via inspector

    private void Start()
    {
        // Add listeners for Android buttons
        if (primaryAttackButton != null)
        {
            primaryAttackButton.onClick.AddListener(() => abilityManager.UseAbilityByTag(currentTag, AbilityType.Primary));
        }
        if (secondaryAttackButton != null)
        {
            secondaryAttackButton.onClick.AddListener(() => abilityManager.UseAbilityByTag(currentTag, AbilityType.Secondary));
        }
    }

    private void Update()
    {
        // Check for mouse button inputs
        if (Input.GetMouseButtonDown(0)) // Left Click
        {
            abilityManager.UseAbilityByTag(currentTag, AbilityType.Primary); // Use first ability
        }
        if (Input.GetMouseButtonDown(1)) // Right Click
        {
            abilityManager.UseAbilityByTag(currentTag, AbilityType.Secondary); // Use second ability
        }
    }
}

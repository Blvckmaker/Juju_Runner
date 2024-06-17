using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    [System.Serializable]
    public struct AbilityPair
    {
        public AbilityData ability1;
        public AbilityData ability2;
    }

    [SerializeField] private List<AbilityPair> abilityPairs;
    [SerializeField] private List<CharacterTag> tags;

    private Dictionary<CharacterTag, AbilityPair> abilityMap;
    private Ability abilityComponent;

    private void Awake()
    {
        abilityComponent = GetComponent<Ability>();
        abilityMap = new Dictionary<CharacterTag, AbilityPair>();

        for (int i = 0; i < tags.Count; i++)
        {
            abilityMap.Add(tags[i], abilityPairs[i]);
        }
    }

    public void UseAbilityByTag(CharacterTag tag, AbilityType abilityType)
    {
        if (abilityMap.TryGetValue(tag, out AbilityPair abilityPair))
        {
            AbilityData selectedAbility = abilityType == AbilityType.Primary ? abilityPair.ability1 : abilityPair.ability2;

            if (selectedAbility != null)
            {
                abilityComponent.abilityData = selectedAbility;
                abilityComponent.UseAbility(abilityType);
            }
            else
            {
                Debug.LogWarning("Ability not found for ability type: " + abilityType);
            }
        }
        else
        {
            Debug.LogWarning("No ability found for tag: " + tag);
        }
    }
}

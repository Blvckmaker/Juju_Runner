using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModelSwitcher : MonoBehaviour
{
    [System.Serializable]
    public struct PlayerModel
    {
        public GameObject playerMesh;
        public Avatar playerAvatar;
    }

    [SerializeField] private PlayerModel[] playerModels;
    [SerializeField] private int selectedCharacterIndex = 0;

    void Start()
    {
        SwitchModel(selectedCharacterIndex);
    }

    void OnValidate()
    {
        SwitchModel(selectedCharacterIndex);
    }

    public void SwitchModel(int index)
    {
        if (index < 0 || index >= playerModels.Length)
        {
            Debug.LogWarning("Invalid model index.");
            return;
        }

        // Deactivate all player meshes
        for (int i = 0; i < playerModels.Length; i++)
        {
            if (playerModels[i].playerMesh != null)
            {
                playerModels[i].playerMesh.SetActive(i == index);
            }
        }

        // Set the avatar for the selected model
        var animator = GetComponent<Animator>();
        if (animator != null)
        {
            animator.avatar = playerModels[index].playerAvatar;
        }
    }
}

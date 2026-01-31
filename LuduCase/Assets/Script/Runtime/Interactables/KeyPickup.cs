using UnityEngine;

public class KeyPickup : InteractableBase
{
    [Header("Key Settings")]
    [SerializeField] private string keyID = "Key_A";

    private void Start()
    {
        interactionType = InteractionType.Instant;
        promptMessage = $"Pick up {keyID}";
    }

    public override void OnInteract(GameObject interactor)
    {
        var inventory = interactor.GetComponent<PlayerInventory>();
        if (inventory != null)
        {
            inventory.AddKey(keyID);

            // Görseli kapat veya objeyi yok et
            gameObject.SetActive(false);
        }
    }
}
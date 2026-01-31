using UnityEngine;

public class Door : InteractableBase
{
    [Header("Door Settings")]
    [SerializeField] private bool isLocked = false;
    [SerializeField] private string requiredKeyID = "Key_A";
    [SerializeField] private Animator animator; // Kapý animasyonu için

    private bool isOpen = false;

    private void Start()
    {
        interactionType = InteractionType.Toggle;
    }

    public override bool CanInteract(GameObject interactor)
    {
        if (!isLocked) return true;

        var inventory = interactor.GetComponent<PlayerInventory>();
        if (inventory != null && inventory.HasKey(requiredKeyID))
        {
            return true;
        }

        promptMessage = "Locked (Requires Key)"; // UI Feedback deðiþtiriyoruz
        return false;
    }

    public override void OnInteract(GameObject interactor)
    {
        // Kilitli ise ve anahtar varsa kilidi aç
        if (isLocked)
        {
            isLocked = false;
            promptMessage = "Open Door";
            Debug.Log("Door Unlocked!");
            return; // Ýlk týklama kilidi açar, ikinci týklama kapýyý açar (tercihe baðlý)
        }

        isOpen = !isOpen;
        promptMessage = isOpen ? "Close Door" : "Open Door";

        // Animasyon tetiklemesi
        if (animator) animator.SetBool("IsOpen", isOpen);

        Debug.Log("Door is " + (isOpen ? "Open" : "Closed"));
    }
}
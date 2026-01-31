using UnityEngine;

public class Chest : InteractableBase
{
    [Header("Chest Settings")]
    [SerializeField] private GameObject lootPrefab; // Ýçinden çýkacak eþya
    private bool isOpen = false;

    private void Start()
    {
        interactionType = InteractionType.Hold;
        holdDuration = 2.0f; // 2 saniye basýlý tutulmalý
        promptMessage = "Hold to Open Chest";
    }

    public override void OnInteract(GameObject interactor)
    {
        if (isOpen) return;

        isOpen = true;
        Debug.Log("Chest Opened!");

        // Kapaðý açma animasyonu...
        // transform.Find("Lid").Rotate(-90, 0, 0);

        // Loot spawn etme
        if (lootPrefab != null)
        {
            Instantiate(lootPrefab, transform.position + Vector3.up, Quaternion.identity);
        }

        // Artýk etkileþime girilemez yapalým
        promptMessage = "Empty";
        GetComponent<Collider>().enabled = false; // Basitçe collider kapatýlabilir
    }
}
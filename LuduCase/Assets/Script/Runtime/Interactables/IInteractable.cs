using UnityEngine;

// Etkileþim türlerini belirleyen Enum
public enum InteractionType
{
    Instant, // Anýnda (Eþya toplama)
    Hold,    // Basýlý tutma (Sandýk açma)
    Toggle   // Aç/Kapa (Iþýk, Kapý)
}

// Tüm etkileþime girilebilir nesnelerin uymasý gereken sözleþme
public interface IInteractable
{
    string InteractionPrompt { get; }      // Ekranda görünecek yazý (Örn: "Open Chest")
    InteractionType Type { get; }          // Etkileþim türü
    float HoldDuration { get; }            // Hold ise kaç saniye süreceði

    bool CanInteract(GameObject interactor); // Etkileþime girilebilir mi? (Örn: Kilitli mi?)
    void OnInteract(GameObject interactor);  // Etkileþim gerçekleþtiðinde ne olacak?
    void OnFocus();                          // Oyuncu nesneye baktýðýnda
    void OnLoseFocus();                      // Oyuncu bakmayý býraktýðýnda
}
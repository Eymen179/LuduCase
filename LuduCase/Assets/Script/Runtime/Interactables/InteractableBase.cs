using UnityEngine;

public abstract class InteractableBase : MonoBehaviour, IInteractable
{
    [SerializeField] protected string promptMessage = "Interact";
    [SerializeField] protected InteractionType interactionType = InteractionType.Instant;
    [SerializeField] protected float holdDuration = 0f;

    protected Outline outline;

    public string InteractionPrompt => promptMessage;
    public InteractionType Type => interactionType;
    public float HoldDuration => holdDuration;

    protected virtual void Awake()
    {
        // 1. Ayný obje üzerinde Outline arýyoruz
        outline = GetComponent<Outline>();

        if (outline != null)
        {
            outline.enabled = false; // Baþlangýçta kapat
            outline.OutlineWidth = 5f;
            Debug.Log($"{gameObject.name}: Outline bulundu ve kapatýldý.");
        }
        else
        {
            // Eðer bulamazsa kýrmýzý hata verir
            Debug.LogError($"{gameObject.name}: Interactable scripti var ama Outline scripti EKSÝK!");
        }
    }

    public virtual bool CanInteract(GameObject interactor) => true;
    public abstract void OnInteract(GameObject interactor);

    public virtual void OnFocus()
    {
        if (outline != null) outline.enabled = true;
    }

    public virtual void OnLoseFocus()
    {
        if (outline != null) outline.enabled = false;
    }
}
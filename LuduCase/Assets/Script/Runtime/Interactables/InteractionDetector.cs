using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem; // Yeni Input Sistemi Kütüphanesi

public class InteractionDetector : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float interactionRange = 3f;
    [SerializeField] private LayerMask interactableLayer;

    // DEÐÝÞÝKLÝK 1: KeyCode yerine InputActionReference kullanýyoruz
    [SerializeField] private InputActionReference interactAction;

    [SerializeField] private Transform cameraTransform;

    [Header("UI References")]
    [SerializeField] private GameObject interactionPanel;
    [SerializeField] private TextMeshProUGUI promptText;
    [SerializeField] private Image holdProgressBar;

    private IInteractable currentInteractable;
    private float currentHoldTimer = 0f;
    private bool isHolding = false;

    // DEÐÝÞÝKLÝK 2: Input Action'ý etkinleþtirmemiz gerekiyor
    private void OnEnable()
    {
        if (interactAction != null && interactAction.action != null)
            interactAction.action.Enable();
    }

    private void OnDisable()
    {
        if (interactAction != null && interactAction.action != null)
            interactAction.action.Disable();
    }

    private void Update()
    {
        HandleRaycast();
        HandleInput();
    }

    private void HandleRaycast()
    {
        Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
        RaycastHit hit;

        bool hitSomething = Physics.Raycast(ray, out hit, interactionRange, interactableLayer);

        if (hitSomething)
        {
            Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.green);
        }
        else
        {
            Debug.DrawRay(ray.origin, ray.direction * interactionRange, Color.red);
        }

        if (hitSomething)
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();

            if (interactable != null)
            {
                if (currentInteractable != interactable)
                {
                    if (currentInteractable != null) currentInteractable.OnLoseFocus();

                    currentInteractable = interactable;
                    currentInteractable.OnFocus();
                    UpdateUI(true);
                    Debug.Log($"Görülen Nesne: {hit.collider.gameObject.name}");
                }
                return;
            }
        }

        if (currentInteractable != null)
        {
            currentInteractable.OnLoseFocus();
            currentInteractable = null;
            UpdateUI(false);
            isHolding = false;
            currentHoldTimer = 0f;
        }
    }

    private void HandleInput()
    {
        if (currentInteractable == null) return;

        // Input Action null kontrolü
        if (interactAction == null || interactAction.action == null)
        {
            Debug.LogWarning("Interaction Action atanmamýþ!");
            return;
        }

        if (!currentInteractable.CanInteract(this.gameObject))
        {
            promptText.text = "Locked";
            holdProgressBar.fillAmount = 0;
            return;
        }

        switch (currentInteractable.Type)
        {
            case InteractionType.Instant:
            case InteractionType.Toggle:
                // DEÐÝÞÝKLÝK 3: Input.GetKeyDown -> WasPressedThisFrame
                if (interactAction.action.WasPressedThisFrame())
                {
                    currentInteractable.OnInteract(this.gameObject);
                }
                break;

            case InteractionType.Hold:
                // DEÐÝÞÝKLÝK 4: Input.GetKey -> IsPressed
                if (interactAction.action.IsPressed())
                {
                    isHolding = true;
                    currentHoldTimer += Time.deltaTime;
                    holdProgressBar.fillAmount = currentHoldTimer / currentInteractable.HoldDuration;

                    if (currentHoldTimer >= currentInteractable.HoldDuration)
                    {
                        currentInteractable.OnInteract(this.gameObject);
                        ResetHold();
                    }
                }
                else
                {
                    ResetHold();
                }
                break;
        }
    }

    private void ResetHold()
    {
        isHolding = false;
        currentHoldTimer = 0f;
        holdProgressBar.fillAmount = 0f;
    }

    private void UpdateUI(bool isActive)
    {
        if (interactionPanel) interactionPanel.SetActive(isActive);

        if (isActive && currentInteractable != null && promptText)
        {
            // DEÐÝÞÝKLÝK 5: Tuþ ismini dinamik olarak Action'dan çekiyoruz
            string keyName = interactAction != null ? interactAction.action.GetBindingDisplayString() : "KEY";
            promptText.text = $"{currentInteractable.InteractionPrompt} [{keyName}]";
            holdProgressBar.fillAmount = 0f;
        }
    }
}
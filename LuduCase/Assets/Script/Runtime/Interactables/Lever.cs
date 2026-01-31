using UnityEngine;
using UnityEngine.Events;

public class Lever : InteractableBase
{
    [Header("Lever Events")]
    public UnityEvent onToggleOn;
    public UnityEvent onToggleOff;

    private bool isOn = false;

    private void Start()
    {
        interactionType = InteractionType.Toggle;
        promptMessage = "Pull Lever";
    }

    public override void OnInteract(GameObject interactor)
    {
        isOn = !isOn;

        // Kolun görsel rotasyonu burada yapýlabilir
        transform.localEulerAngles = new Vector3(isOn ? 45 : -45, 0, 0);

        if (isOn)
            onToggleOn.Invoke();
        else
            onToggleOff.Invoke();

        Debug.Log($"Lever pulled. State: {isOn}");
    }
}
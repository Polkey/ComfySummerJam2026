using System;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    public Transform InteractorSource;
    public float InteractRange;
    private IInteractable currentlyHighlighted;
    void Update() {
        Ray r = new Ray(InteractorSource.position, InteractorSource.forward);

        if (Physics.Raycast(r, out RaycastHit hitInfo, InteractRange) &&
            hitInfo.collider.TryGetComponent(out IInteractable interactObj)) {
                currentlyHighlighted = interactObj;
                currentlyHighlighted.Highlight();
            if (Input.GetMouseButtonDown(0)) {
                currentlyHighlighted.Interact();
            }
        }
        else {
            currentlyHighlighted?.Unhighlight();
            currentlyHighlighted = null;
        }
    }
    public void Interact() {
        Ray r = new Ray(InteractorSource.position, InteractorSource.forward);
        if(Physics.Raycast(r, out RaycastHit hitInfo, InteractRange)) {
            if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObj)) {
                interactObj.Interact();
            }
        }
    }
}

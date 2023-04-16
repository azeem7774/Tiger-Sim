
using UnityEngine;

public class TriggerDetection : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var thing = other.gameObject.GetComponent<IInteractable>();
        if (thing != null)
        {
            thing.Interact();
        }
    }
}

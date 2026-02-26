using UnityEngine;
using UnityEngine.Events;

public class VehicleCollision : MonoBehaviour
{
    public string tagToCheck;
    public UnityEvent OnCollision;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != tagToCheck) return;
        OnCollision?.Invoke();
    }
}

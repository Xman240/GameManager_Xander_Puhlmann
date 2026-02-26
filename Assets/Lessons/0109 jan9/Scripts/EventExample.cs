using UnityEngine;
using UnityEngine.Events;

public class EventExample : MonoBehaviour
{
    public UnityEvent onCoinCollected;

    public void CollectCoin()
    {
        onCoinCollected?.Invoke();
    }
}

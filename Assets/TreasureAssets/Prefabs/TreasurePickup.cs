using System;
using UnityEngine;

public class TreasurePickup : MonoBehaviour
{
    public static event Action PickupTreasure;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PickupTreasure?.Invoke();
            Destroy(gameObject);
            Debug.Log("Picked up Treasure");
        }
    }
}

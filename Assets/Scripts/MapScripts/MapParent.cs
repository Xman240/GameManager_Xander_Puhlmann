using UnityEngine;

public class MapParent : MonoBehaviour
{
    private void Awake()
    {
        if (GameStateManager.Instance != null)
        {
            GameStateManager.Instance.mapParent = transform;
        }
    }
}

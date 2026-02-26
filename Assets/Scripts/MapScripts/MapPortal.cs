using Unity.Cinemachine;
using UnityEngine;

public class MapPortal : MonoBehaviour
{
    public int targetMapID = 0;
    public int targetPortalID = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Player") return;
        Debug.Log("Hit Player");
        MapNavigation.Instance.GoToMap(targetMapID, targetPortalID);
    }
}

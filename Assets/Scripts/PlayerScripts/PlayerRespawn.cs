using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
   public PlayerHealth playerHealth;
   public MapNavigation mapNavigation;
   public int respawnMapId;
   public int respawnPortalId;

   private void Awake()
   {
      if (playerHealth == null)
      {
         playerHealth = GetComponent<PlayerHealth>();
      }
   }

   private void OnEnable()
   {
      if (playerHealth != null)
      {
         playerHealth.OnDeath += Respawn;
      }
   }
   
   private void OnDisable()
   {
      if (playerHealth != null)
      {
         playerHealth.OnDeath -= Respawn;
      }
   }

   private void Respawn()
   {
      playerHealth.HealFull();
      mapNavigation.GoToMap(respawnMapId, respawnPortalId);
   }
}

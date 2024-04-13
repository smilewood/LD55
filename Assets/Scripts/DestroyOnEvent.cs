using UnityEngine;

namespace LD55
{
   public class DestroyOnEvents : MonoBehaviour
   {
      public void DestroyMe()
      {
         Destroy(this.gameObject);
      }

      public void OnZeroHealth (HealthTracker hp, int _)
      {
         if (hp.CurrentHealth <= 0)
         {
            DestroyMe();
         }
      }
   }
}

using UnityEngine;
using UnityEngine.Events;

namespace LD55
{
   public class DestroyOnEvents : MonoBehaviour
   {
      public UnityEvent OnDeath;
      public void DestroyMe()
      {
         OnDeath?.Invoke();
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD55
{
   public class EnemyDeath : MonoBehaviour
   {
      public void OnHealthChange(HealthTracker hp, int _)
      {
         if(hp.CurrentHealth <= 0)
         {
            Destroy(this.gameObject);
         }
      }
   }
}

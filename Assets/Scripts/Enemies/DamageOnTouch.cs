using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace LD55
{
   public class DamageOnTouch : MonoBehaviour
   {
      public string TypeToDamage;
      public int DamageAmount;
      public UnityEvent OnDealDamage;

      public float ManaOnKill;

      private void OnCollisionEnter(Collision collision)
      {
         if(collision.gameObject.CompareTag(TypeToDamage))
         {
            if(collision.gameObject.TryGetComponent(out HealthTracker hp))
            {
               hp.ApplyDamage(DamageAmount);
               if(hp.CurrentHealth <= 0)
               {
                  PlayerMana.ManaChangeEvent.Invoke(ManaOnKill);
               }
            }
            OnDealDamage?.Invoke();
         }
      }
   }
}

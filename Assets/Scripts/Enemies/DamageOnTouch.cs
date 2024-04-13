using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD55
{
   public class DamageOnTouch : MonoBehaviour
   {
      public string TypeToDamage;
      public int DamageAmount;
      public bool DestroyMyselfOnDamage;

      private void OnCollisionEnter(Collision collision)
      {
         Debug.Log($"{gameObject.name} collided with {collision.gameObject.name}");
         if(collision.gameObject.CompareTag(TypeToDamage))
         {
            if(collision.gameObject.TryGetComponent(out HealthTracker hp))
            {
               hp.ApplyDamage(DamageAmount);
            }

            if (DestroyMyselfOnDamage)
            {
               Destroy(this.gameObject);
            }
         }
      }
   }
}

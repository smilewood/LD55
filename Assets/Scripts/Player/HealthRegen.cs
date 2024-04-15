using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD55
{
   [RequireComponent(typeof(HealthTracker))]
   public class HealthRegen : MonoBehaviour
   {
      public int HealthGain;
      public float RegenTime;
      private HealthTracker hp;
      // Start is called before the first frame update
      void Start()
      {
         hp = this.gameObject.GetComponent<HealthTracker>();
      }

      public void BeginRegen()
      {
         StartCoroutine(RegenLoop());
      }

      private IEnumerator RegenLoop()
      {
         while (true)
         {
            yield return new WaitForSeconds(RegenTime);
            hp.ApplyDamage(-HealthGain);
         }
      }
   }
}

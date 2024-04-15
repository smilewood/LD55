using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD55
{
   public class SummonManaLock : MonoBehaviour, IStatusBarCheckable
   {
      private void Update()
      {
         float lockAmount = 0;
         foreach (Transform child in this.transform)
         {
            lockAmount += child.gameObject.GetComponent<Spawnable>().ManaLock;
         }
         PlayerMana.Instance.LockedMana = lockAmount;
      }

      public float CurrentPercent
      {
         get
         {
            return PlayerMana.Instance.LockedMana / PlayerMana.Instance.MaxMana;
         }
      }
   }
}

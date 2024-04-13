using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace LD55
{
   /// <summary>
   /// Notification that health changed, and by how much
   /// </summary>
   [Serializable]
   public class HealthEvent : UnityEvent<HealthTracker, int> {}

   public class HealthTracker: MonoBehaviour
   {
      public int MaxHealth;
      public int CurrentHealth
      {
         get; private set;
      }
      public HealthEvent OnHealthChange;

      private void Start()
      {
         CurrentHealth = MaxHealth;
      }

      public void ApplyDamage(int damageAmount)
      {
         CurrentHealth -= damageAmount;
         OnHealthChange?.Invoke(this, damageAmount);
         Debug.Log($"{gameObject.name} took {damageAmount} points of damage");
      }


   }
}
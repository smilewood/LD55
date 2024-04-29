using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD55
{
   public class PlayerUpgrader : MonoBehaviour
   {
      private GameObject player;

      [Header("Durability")]
      public int NewHP;
      public int HpRegenAmount;
      public float HpReginTime;


      [Header("Magic")]
      public float NewMana;
      public float NewManaRegenAmount;
      public float NewManaRegenTime;

      [Header("Flight")]
      public float NewMoveSpeed;
      public float NewFlightDrain;


      // Start is called before the first frame update
      void Start()
      {
         player = GameObject.Find("PlayerRoot");
      }

      public void Durability()
      {
         player.GetComponent<HealthTracker>().MaxHealth = NewHP;
         HealthRegen regen = player.AddComponent<HealthRegen>();
         regen.HealthGain = HpRegenAmount;
         regen.RegenTime = HpReginTime;
         regen.BeginRegen();
      }

      public void Magic()
      {
         PlayerMana.Instance.MaxMana = NewMana;
         PlayerMana.Instance.ManaPerTime = NewManaRegenAmount;
         PlayerMana.Instance.TimeInterval = NewManaRegenTime;
      }

      public void Fligh()
      {
         player.GetComponent<PlayerMovement>().speed = NewMoveSpeed;
         var edgeTracker = player.GetComponent<FallOffEdge>();
         edgeTracker.OverTheEdge.RemoveAllListeners();
         edgeTracker.OverTheEdge.AddListener(() => PlayerMana.Instance.FixedDrainMana(NewFlightDrain));
      }

      public void LimitBreak()
      {
         PlayerMana.Instance.ManaLocking = false;
      }

   }
}

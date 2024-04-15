using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace LD55
{
   public class PlayerMana : MonoBehaviour, IStatusBarCheckable
   {
      public static UnityEvent<float> ManaChangeEvent
      {
         get
         {
            _manaChangeEvent ??= new UnityEvent<float>();
            return _manaChangeEvent;
         }
      }
      private static UnityEvent<float> _manaChangeEvent;

      public static PlayerMana Instance
      {
         get; private set;
      }
      private void Awake()
      {
         Debug.Assert(Instance == null, "There should only be one PlayerMana instance, some info may be lost");
         Instance = this;
      }

      public float MaxMana;
      public float LockedMana;
      public float LockBuffer;
      public float ManaPerTime;
      public float TimeInterval;
      public bool ManaLocking = true;
      public float CurrentMana
      {
         get; private set;
      }
      public float CurrentPercent
      {
         get
         {
            return CurrentMana / MaxMana;
         }
      }

      // Start is called before the first frame update
      void Start()
      {
         CurrentMana = MaxMana;
         ManaChangeEvent.AddListener(ChangeMana);
         StartCoroutine(GainManaPerTime());
      }

      // Update is called once per frame
      void Update()
      {
      }

      private IEnumerator GainManaPerTime()
      {
         while (true)
         {
            yield return new WaitForSeconds(TimeInterval);
            ChangeMana(ManaPerTime);
         }
      }
      public UnityEvent OutOfMana;
      public void ChangeMana(float amount)
      {
         CurrentMana = Mathf.Min(CurrentMana + amount, MaxMana - Mathf.Clamp(LockedMana - LockBuffer, 0, ManaLocking ? MaxMana : 0));
         if(CurrentMana < 0)
         {
            OutOfMana.Invoke();
         }
      }

      public void DrainMana(float amount)
      {
         ChangeMana(-amount * Time.deltaTime);
      }
   }
}

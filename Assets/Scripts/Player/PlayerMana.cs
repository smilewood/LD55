using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace LD55
{
   public class PlayerMana : MonoBehaviour
   {
      public static UnityEvent<int> ManaChangeEvent
      {
         get
         {
            _manaChangeEvent ??= new UnityEvent<int>();
            return _manaChangeEvent;
         }
      }
      private static UnityEvent<int> _manaChangeEvent;

      public int MaxMana;
      public int ManaPerTime;
      public float TimeInterval;
      public int CurrentMana
      {
         get; private set;
      }

      // Start is called before the first frame update
      void Start()
      {
         CurrentMana = MaxMana;
         ManaChangeEvent.AddListener(ChangeMana);
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

      private void ChangeMana(int amount)
      {
         CurrentMana = Mathf.Clamp(CurrentMana + amount, 0, MaxMana);
      }
   }
}

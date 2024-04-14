using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;

namespace LD55
{
   public class ArenaManager : MonoBehaviour
   {
      public List<GameObject> ActiveEnemies;
      public UnityEvent OnArenaClear;

      // Start is called before the first frame update
      void Start()
      {
         foreach(GameObject enemy in ActiveEnemies)
         {
            enemy.GetComponent<DestroyOnEvents>().OnDeath.AddListener(() => OnEnemyDamage(enemy));
         }
      }

      private void OnEnemyDamage(GameObject enemy)
      {
         ActiveEnemies.Remove(enemy);
         if (!ActiveEnemies.Any())
         {
            //All enemies are gone
            OnArenaClear?.Invoke();
            SoundManager.GlobalSoundManager.PlaySound(SoundOrMusic.LevelCleared);
         }
      }
   }
}

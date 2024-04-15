using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;

namespace LD55
{
   public class ArenaManager : MonoBehaviour
   {
      private List<GameObject> ActiveEnemies;
      public GameObject EnemyParent;

      public UnityEvent OnArenaClear;
      public GameObject NextArena;
      public static ArenaManager ActiveArena;
      public bool FinalLevel;
      // Start is called before the first frame update
      void Start()
      {
         ActiveArena = this;
         ActiveEnemies = EnemyParent.transform.Cast<Transform>().Select((t) => t.gameObject).ToList();
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
            SoundManager.GlobalSoundManager.PlaySound(SoundOrMusic.LevelCleared);
            OnArenaClear?.Invoke();
            if (!FinalLevel)
            {
               StartCoroutine(DelayLevelClear());
            }
         }
      }

      public IEnumerator DelayLevelClear()
      {
         yield return new WaitForSeconds(1);
         MenuFunctions.Instance.ShowMenu("UpgradeMenu");
         MenuFunctions.PauseGame(false);
      }

      public void SpawnNextArena()
      {
         GameObject level = Instantiate(NextArena, this.transform.parent);
         GameObject.Find("PlayerRoot").transform.position = level.transform.Find("SpawnPoint").position;
         GameObject.Find("SummonParent").transform.position = level.transform.Find("SpawnPoint").position;
         Destroy(this.gameObject);
      }
   }
}

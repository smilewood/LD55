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
            foreach (GameObject enemy in ActiveEnemies)
            {
                enemy.GetComponent<DestroyOnEvents>().OnDeath.AddListener(() => OnEnemyDamage(enemy));
            }


            BloodSplatter BloodSplatterManager = FindObjectOfType<BloodSplatter>();
            if (BloodSplatterManager == null)
            {
                Debug.LogError("BloodSplatterManager not found");
            }
            else
            {
                OnArenaClear.AddListener(BloodSplatterManager.ClearBlood);
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
         Vector3 spawn = level.transform.Find("SpawnPoint").position;
         Transform summonParent = GameObject.Find("SummonParent").transform;
         List<GameObject> oldSpawns = summonParent.Cast<Transform>().Select(t => t.gameObject).ToList();
         foreach (GameObject summon in oldSpawns)
         {
            Instantiate(summon, spawn, Quaternion.identity, summonParent);
            Destroy(summon);
         }
         Destroy(this.gameObject);
      }
   }
}

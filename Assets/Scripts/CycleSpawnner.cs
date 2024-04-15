using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD55
{
   public class CycleSpawnner : MonoBehaviour
   {
      public DestroyOnEvents ThingToSpawn;
      public float timeBetweenSpawns;

      private void Start()
      {
         GameObject pickup = Instantiate(ThingToSpawn.gameObject, this.transform);
         pickup.GetComponent<DestroyOnEvents>().OnDeath.AddListener(() => StartCoroutine(SpawnCycle()));
      }

      public IEnumerator SpawnCycle()
      {
         yield return new WaitForSeconds(timeBetweenSpawns);

         GameObject pickup = Instantiate(ThingToSpawn.gameObject, this.transform);
         pickup.GetComponent<DestroyOnEvents>().OnDeath.AddListener(() => StartCoroutine(SpawnCycle()));
      }

      private void OnDestroy()
      {
         StopAllCoroutines();  
      }

      private void OnDrawGizmos()
      {
         Gizmos.DrawWireCube(this.transform.position, Vector3.one / 2);
      }

   }
}

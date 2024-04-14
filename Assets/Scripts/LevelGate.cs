using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD55
{
   public class LevelGate : MonoBehaviour
   {
      private BoxCollider gateCollider;
      public GameObject NextLevel;


      // Start is called before the first frame update
      void Start()
      {
         gateCollider = GetComponent<BoxCollider>();
      }

      public void UnlockGate()
      {
         gateCollider.isTrigger = true;
      }

      private void OnTriggerEnter(Collider other)
      {
         GameObject otherRoot = other.GetComponentInParent<Rigidbody>().gameObject;
         if (otherRoot.CompareTag("Player"))
         {
            GameObject level = Instantiate(NextLevel, this.transform.parent.parent);
            otherRoot.transform.position = level.transform.Find("SpawnPoint").position;
            GameObject.Find("SummonParent").transform.position = level.transform.Find("SpawnPoint").position;
            Destroy(this.transform.parent.gameObject);
         }
      }
   }
}

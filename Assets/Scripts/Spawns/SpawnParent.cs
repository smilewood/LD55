using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD55
{
   public class SpawnParent : MonoBehaviour
   {
      // Update is called once per frame
      void Update()
      {
         if (this.transform.childCount == 0)
         {
            Destroy(this.gameObject);
         }
      }
   }
}

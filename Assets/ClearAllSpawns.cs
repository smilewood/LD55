using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD55
{
   public class ClearAllSpawns : MonoBehaviour
   {
      // Start is called before the first frame update
      void Start()
      {
         foreach (Transform t in this.transform.parent)
         {
            if(t.gameObject != this.gameObject)
            {
               Destroy(t.gameObject);
            }
         }
         Destroy(this.gameObject);
      }

   }
}

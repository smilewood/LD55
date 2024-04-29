using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD55
{
   public class DestroyOnAnimationComplete : MonoBehaviour
   {
      public void DeathFinished()
      {
         if(!transform.parent.gameObject.TryGetComponent<DestroyOnAnimationComplete>(out DestroyOnAnimationComplete parent))
         {
            Destroy(transform.parent.gameObject);
         }
         else
         {
            parent.DeathFinished();
         }
      }
   }
}

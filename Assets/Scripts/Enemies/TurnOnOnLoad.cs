using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD55
{
   /// <summary>
   /// This is a stupid solution to a stupid issue (the collider keeps turning off)
   /// but we need this to work, so here it is.
   /// </summary>
   public class TurnOnOnLoad : MonoBehaviour
   {


      // Start is called before the first frame update
      void Start()
      {
         this.GetComponent<SphereCollider>().enabled = true;
      }
   }
}

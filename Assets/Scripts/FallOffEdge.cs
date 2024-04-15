using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace LD55
{
   [Serializable]
   public class FallOffEdgeEvent : UnityEvent {}
   public class FallOffEdge : MonoBehaviour
   {
      public FallOffEdgeEvent OverTheEdge;
      
      // Update is called once per frame
      void Update()
      {
         Debug.DrawLine(transform.position, transform.position + (Vector3.down * 2));
         if (!Physics.Raycast(new Ray(transform.position, Vector3.down), out RaycastHit hit, 2) || !hit.collider.gameObject.CompareTag("Floor"))
         {
            OverTheEdge?.Invoke();
           // Debug.Log($"{gameObject.name} Fell off the edge!");
         }
      }
   }
}

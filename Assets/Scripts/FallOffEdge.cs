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
      public FallOffEdgeEvent OnFallOffEdge;
      
      // Update is called once per frame
      void Update()
      {
         if(!Physics.Raycast(new Ray(transform.position, Vector3.down), out RaycastHit hit, 1) || !hit.collider.gameObject.CompareTag("Floor"))
         {
            OnFallOffEdge?.Invoke();
            Debug.Log($"{gameObject.name} Fell off the edge!");
         }
      }
   }
}

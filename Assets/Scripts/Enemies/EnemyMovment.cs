using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD55
{
   [RequireComponent(typeof(EnemyTracking))]
   public class EnemyMovment : MonoBehaviour
   {
      public float speed;
      private EnemyTracking tracking;

      private void Start()
      {
         this.tracking = GetComponent<EnemyTracking>();
      }

      private void FixedUpdate()
      {
         Vector3 delta = Vector3.Normalize(tracking.TargetPosition - this.transform.position);
         this.transform.position = this.transform.position + (Time.fixedDeltaTime * speed * delta);
      }
   }
}

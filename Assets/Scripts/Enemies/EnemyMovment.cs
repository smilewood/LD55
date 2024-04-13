using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD55
{
   [RequireComponent(typeof(EnemyTracking))]
   [RequireComponent(typeof(Rigidbody))]
   public class EnemyMovment : MonoBehaviour
   {
      public float speed;
      private EnemyTracking tracking;
      private Rigidbody RB;
      private void Start()
      {
         this.tracking = GetComponent<EnemyTracking>();
         this.RB = GetComponent<Rigidbody>();
      }

      private void FixedUpdate()
      {
         Vector3 delta = Vector3.Normalize(tracking.TargetPosition - this.transform.position);
         RB.velocity = speed * delta;
      }
   }
}

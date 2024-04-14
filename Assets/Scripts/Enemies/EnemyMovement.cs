using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD55
{
   [RequireComponent(typeof(EnemyTracking))]
   [RequireComponent(typeof(Rigidbody))]
   public class EnemyMovement : MonoBehaviour
   {
      public float speed;
      public float LookSpeed;
      private EnemyTracking tracking;
      private Rigidbody RB;
      private void Start()
      {
         this.tracking = GetComponent<EnemyTracking>();
         this.RB = GetComponent<Rigidbody>();
      }

      private void Update()
      {
         Quaternion lookTarget = Quaternion.LookRotation(tracking.TargetPosition - transform.position, Vector3.up);
         this.transform.rotation = Quaternion.Lerp(transform.rotation, lookTarget, LookSpeed * Time.deltaTime);
      }

      private void FixedUpdate()
      {
         Vector3 delta = Vector3.Normalize(tracking.TargetPosition - this.transform.position);
         RB.velocity = speed * delta;
      }
   }
}

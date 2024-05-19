using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD55
{
   public class SeekerSwarmAI : MonoBehaviour
   {
      static Vector3 swarmTarget;
      static Vector3 nextSwarmTarget = Vector3.zero;
      static int swarmPop;

      private HealthTracker agroTarget;
      public float Acceleration;
      public float MaxSpeed;
      private Rigidbody RB;
      private Vector3 targetPos;

      // Start is called before the first frame update
      void Start()
      {
         if(swarmTarget == null)
         {
            swarmTarget = this.transform.position;
         }
         this.RB = GetComponent<Rigidbody>();
      }

      // Update is called once per frame
      void Update()
      {
         targetPos = swarmTarget;

         if(agroTarget != null)
         {
            targetPos = agroTarget.transform.position;
         }

         targetPos.y = transform.position.y;
         
         
         nextSwarmTarget += this.transform.position;
         ++swarmPop;
      }

      private void LateUpdate()
      {
         if(swarmPop > 0)
         {
            nextSwarmTarget /= swarmPop;

            swarmTarget = Vector3.Lerp(nextSwarmTarget, PlayerMouseActions.GetMousePositionInWorld(), .5f);
            swarmTarget.y = nextSwarmTarget.y;
            nextSwarmTarget = Vector3.zero;
            
            swarmPop = 0;
         }
      }
      private void FixedUpdate()
      {
         //move towards mouse position
         Vector3 delta = targetPos - this.transform.position;

         RB.velocity += delta.normalized * Acceleration;

         RB.velocity = Vector3.ClampMagnitude(RB.velocity, MaxSpeed);

         Debug.DrawLine(transform.position, targetPos);
      }

      private void OnTriggerEnter(Collider other)
      {
         if(agroTarget == null && other.transform.parent.gameObject.CompareTag("Enemy"))
         {
            agroTarget = other.transform.parent.gameObject.GetComponent<HealthTracker>();
            agroTarget.OnHealthChange.AddListener(OnAgroHealthChange);
            Debug.Log($"{this.gameObject.name} gained agro on {agroTarget.gameObject.name}");
         }
      }

      private void OnAgroHealthChange(HealthTracker t, int _)
      {
         if(t.CurrentHealth <= 0)
         {
            agroTarget = null;
         }
      }

      private void OnDestroy()
      {
         if(agroTarget != null)
         {
            agroTarget.OnHealthChange.RemoveListener(OnAgroHealthChange);
         }
      }
   }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

namespace LD55
{
   public class FollowPlayerTracking : EnemyTracking
   {
      private Transform target;
      public float UpdateTargetTimeInterval = 0.5f;
      public float closeEnoughRadius;
      
      protected override void Start()
      {
         base.Start();

         target = FindTargetPosition();
         Debug.Assert(target != null, "Scene should have a player in it.");

         StartCoroutine(UpdateTargetPosition());
      }

      // Update is called once per frame
      protected override void Update()
      {
         base.Update();
         //Just target the player current position
         //this.TargetPosition = target.position;
         var closeEnoughVector = (transform.position - target.position).normalized * closeEnoughRadius;
         this.SetTarget(closeEnoughVector + target.position);
      }

      
      private IEnumerator UpdateTargetPosition()
      {
         while (true)
         {
            yield return new WaitForSeconds(UpdateTargetTimeInterval);
            target = FindTargetPosition();
         }
      }
      

   }
}

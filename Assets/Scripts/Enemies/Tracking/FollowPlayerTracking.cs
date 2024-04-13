using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

namespace LD55
{
   public class FollowPlayerTracking : EnemyTracking
   {
      private Transform target;

      private NavMeshAgent navMeshAgent;

      private void Start()
      {
         target = GameObject.Find("PlayerRoot").GetComponent<Transform>();
         Debug.Assert(target != null, "Scene should have a player in it.");

         navMeshAgent = this.GetComponentInParent<NavMeshAgent>();
         Debug.Assert(navMeshAgent != null, "NavMeshAgent not loaded.");
      }

      // Update is called once per frame
      void Update()
      {
         //Just target the player current position
         //this.TargetPosition = target.position;

         Debug.Log($"NavMeshAgent destination: ({target.position.x}, {target.position.y}, {target.position.z})");
         this.navMeshAgent.SetDestination(target.position);
      }
   }
}

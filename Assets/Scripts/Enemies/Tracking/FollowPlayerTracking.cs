using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD55
{
   public class FollowPlayerTracking : EnemyTracking
   {
      private Transform target;

      private void Start()
      {
         target = GameObject.Find("PlayerRoot").GetComponent<Transform>();
         Debug.Assert(target != null, "Scene should have a player in it.");
      }


      // Update is called once per frame
      void Update()
      {
         //Just target the player current position
         this.TargetPosition = target.position;
      }
   }
}

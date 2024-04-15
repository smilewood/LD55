using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD55
{
   public class FollowMouseTracking : EnemyTracking
   {
      // Update is called once per frame
      protected override void Update()
      {
         base.Update();
         this.SetTarget(PlayerMouseActions.GetMousePositionInWorld());
      }
   }
}

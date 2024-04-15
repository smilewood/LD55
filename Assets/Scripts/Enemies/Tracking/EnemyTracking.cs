using System.Linq;
using UnityEngine;
using UnityEngine.AI;

namespace LD55
{
   public abstract class EnemyTracking : MonoBehaviour
   {
      public Vector3 TargetPosition { get; protected set; }

      private NavMeshAgent navMeshAgent;


      // Start is called before the first frame update
      virtual protected void Start()
      {
         Debug.Log("StartTracking");
         navMeshAgent = this.GetComponent<NavMeshAgent>();
         navMeshAgent.updatePosition = false;
         navMeshAgent.updateRotation = false;
         Debug.Assert(navMeshAgent != null, "NavMeshAgent not loaded.");
      }

      // Update is called once per frame
      virtual protected void Update()
      {
         this.TargetPosition = navMeshAgent.nextPosition;
         this.TargetPosition = new Vector3(TargetPosition.x, this.transform.position.y, TargetPosition.z);
         Debug.DrawLine(transform.position, TargetPosition);
      }

      protected void SetTarget(Vector3 target)
      {
         this.navMeshAgent.SetDestination(target);
      }
      
      public Transform FindTargetPosition()
      {
         var objs = GameObject.FindGameObjectsWithTag("Player");
         return objs.Where(obj => obj.gameObject.TryGetComponent<EnemyCanAttackMe>(out _))
            .Select(obj => obj.transform)
            .OrderBy(obj => (obj.position - transform.position).magnitude)
            .First();
      }
   }
}

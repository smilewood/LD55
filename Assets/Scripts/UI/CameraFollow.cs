using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD55
{
   public class CameraFollow : MonoBehaviour
   {
      public Transform target;
      public float rubberness = 0.125f;
      public Vector3 offset;
      private Vector3 pos;

      void Awake()
      {
         pos = transform.position;
      }

      private void Start()
      {
         target = GameObject.Find("PlayerRoot").GetComponent<Transform>();
         Debug.Assert(target != null, "Scene should have a player in it.");
         offset = this.transform.position - target.position;
      }

      private float cameraClamp(float value)
      {
         return (Mathf.Floor(value * 32.0f) / 32.0f) + (1f / 64f);
      }

      void FixedUpdate()
      {
         Vector3 desiredPos = target.position + offset;
         pos = Vector3.Lerp(pos, desiredPos, rubberness);
         transform.position = new Vector3(cameraClamp(pos.x), cameraClamp(pos.y), cameraClamp(pos.z));
      }
   }
}

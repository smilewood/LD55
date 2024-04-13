using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD55
{
   [RequireComponent(typeof(Rigidbody))]
   public class PlayerMovement : MonoBehaviour
   {
      public float speed;
      private Vector3 inputDelta;
      private Rigidbody RB;

      // Start is called before the first frame update
      void Start()
      {
         RB = GetComponent<Rigidbody>();
         
      }

      // Update is called once per frame
      void Update()
      {
         inputDelta = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
      }

      private void FixedUpdate()
      {
         RB.velocity = speed * inputDelta;
      }
   }
}

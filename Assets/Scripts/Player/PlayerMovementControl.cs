using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD55
{
   public class PlayerMovement : MonoBehaviour
   {
      public float speed;
      private Vector3 inputDelta;


      // Start is called before the first frame update
      void Start()
      {
         
      }

      // Update is called once per frame
      void Update()
      {
         inputDelta = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
      }

      private void FixedUpdate()
      {
         this.transform.position = this.transform.position + (speed * Time.fixedDeltaTime * inputDelta);
      }
   }
}

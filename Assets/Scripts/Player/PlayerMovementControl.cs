using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace LD55
{
   [RequireComponent(typeof(Rigidbody))]
   public class PlayerMovement : MonoBehaviour
   {
      public float speed;
      private Vector3 inputDelta;
      private Rigidbody RB;
      public float LookSpeed;
      private SpriteParalax sprite;
      public UnityEvent<bool> OnMoveStateChange;

      // Start is called before the first frame update
      void Start()
      {
         RB = GetComponent<Rigidbody>();
         sprite = GetComponentInChildren<SpriteParalax>();
      }

      // Update is called once per frame
      void Update()
      {
         inputDelta = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
         if (inputDelta.magnitude > .001)
         {
            Quaternion lookTarget = Quaternion.LookRotation(-inputDelta, Vector3.up);
            this.transform.rotation = Quaternion.Lerp(transform.rotation, lookTarget, LookSpeed * Time.deltaTime);

            OnMoveStateChange?.Invoke(true);
         }
         else
         {
            OnMoveStateChange?.Invoke(false);
         }
      }

      private void FixedUpdate()
      {
         RB.velocity = speed * inputDelta;
      }
   }
}

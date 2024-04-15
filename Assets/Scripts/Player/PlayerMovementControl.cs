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
      private bool isMoving;
      public UnityEvent<bool> OnMoveStateChange;

      // Start is called before the first frame update
      void Start()
      {
         RB = GetComponent<Rigidbody>();
         sprite = GetComponentInChildren<SpriteParalax>();
         isMoving = false;
         StartCoroutine(MovementSoundChecker());
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
			isMoving = true; 
         }
         else
         {
            OnMoveStateChange?.Invoke(false);
            isMoving = false;
		   }
      }

      private void FixedUpdate()
      {
         RB.velocity = speed * inputDelta.normalized;
      }

      private IEnumerator MovementSoundChecker()
      {
         while (true)
         {
            if (isMoving)
            {
               SoundManager.GlobalSoundManager.PlaySound(SoundOrMusic.FlightWinged);
            }

            yield return new WaitForSeconds(0.5f);
         }
      }
   }
}

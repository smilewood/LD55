using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace LD55
{
    [RequireComponent(typeof(Rigidbody))]
    public class Seeker : MonoBehaviour
    {
        public float moveSpeed = 10.0f;
        private Rigidbody RB;
        private Vector3 targetPos;
        public static int maxSeekerCount = 10;
 
        // Start is called before the first frame update
        void Start()
        {
            //potentially initialize by storing a reference to target, if generalizing
            this.RB = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void Update()
        {
            var result = PlayerMouseActions.GetMousePositionInWorld();
            if (result != Vector3.zero)
            {
                targetPos = result;
                targetPos.y = transform.position.y;
            }
        }

        private void FixedUpdate()
        {
            //move towards mouse position
            Vector3 delta = targetPos - this.transform.position;
            RB.velocity = delta.normalized * Math.Min(moveSpeed, delta.sqrMagnitude * 1.2f);//magic numbers
            
            Debug.DrawLine(transform.position, targetPos);
        }
        
        
        
    }
}

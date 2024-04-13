using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD55
{
    [RequireComponent(typeof(Rigidbody))]
    public class Seeker : MonoBehaviour
    {
        public float moveSpeed = 10.0f;
        private Rigidbody RB;
        private Vector3 targetPos;
        
        // Start is called before the first frame update
        void Start()
        {
            //potentially initialize by storing a reference to target, if generalizing
            this.RB = GetComponent<Rigidbody>();
            
        }

        // Update is called once per frame
        void Update()
        {
            //get mouse position
            var mousePos = Input.mousePosition;
            var cameraRay = Camera.main.ScreenPointToRay(mousePos);
            if (Physics.Raycast(cameraRay, out RaycastHit hitData))
            {
                targetPos = hitData.point;
                targetPos.y = transform.position.y;
            }
        }

        private void FixedUpdate()
        {
            //move towards mouse position
            var newpos = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            RB.MovePosition(newpos);
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace LD55
{
    public class ChargerTracking : EnemyTracking
    {
        //use damage on touch, not melee damage
        public float chargeUpTime;
        public float cooldownTime;
        public float chargeDistance;
        public float chargeDistanceOvershootPercentage;
        public float meleeDistance;
        public float lookSpeedWhilePowerUp;
        public float regularLookSpeed;
        public float regularMoveSpeed;
        public float moveSpeedWhileCharging;
        public float chargeTimeout = 1.0f;
        public enum ChargerState
        {
            Moving,
            PowerUp,
            Charging,
            Resting,
            Melee
        }

        public UnityEvent OnStateChange;
        public ChargerState currentState;
        private EnemyMovement movement;
        void Start()
        {
            //update tracking with new speeds at various points of state mach
            movement = GetComponent<EnemyMovement>();
            navMeshAgent = this.GetComponent<NavMeshAgent>();
            navMeshAgent.updatePosition = false;
            navMeshAgent.updateRotation = false;

            Start_Moving();
        }

        // Update is called once per frame
        void Update()
        {
            var previousState = currentState;
            //state machine - transition logic goes here.
            //this method eventually calls the state transition checks
            CheckTransitionRules();

            if (previousState != currentState)
            {
                Debug.Log("State Transitioned from " + previousState.ToString() + " to " + currentState.ToString());
            }
            //Run state
            RunState();
        }
        
        //make functions for the states here. 

        #region State machine stuff
        void CheckTransitionRules()
        {
            ChargerState oldState = currentState;
            switch (currentState)
            {
                case ChargerState.Charging:
                    CheckTransition_Charging();
                    break;
                case ChargerState.Moving:
                    CheckTransition_Moving();
                    break;
                case ChargerState.Resting:
                    CheckTransition_Resting();
                    break;
                case ChargerState.PowerUp:
                    CheckTransition_PowerUp();
                    break;
                case ChargerState.Melee:
                    CheckTransition_Melee();
                    break;
            }
            if (currentState != oldState)
                OnStateChange?.Invoke();
        }
        
        void RunState()
        {
            switch (currentState)
            {
                case ChargerState.Charging:
                    Update_Charging();
                    break;
                case ChargerState.Moving:
                    Update_Moving();
                    break;
                case ChargerState.Resting:
                    Update_Resting();
                    break;
                case ChargerState.PowerUp:
                    Update_PowerUp();
                    break;
                case ChargerState.Melee:
                    Update_Melee();
                    break;
            }
        }
        #endregion
        
        #region Charging

        private Vector3 chargeStartPosition;
        private bool collidedWithSomething = false;

        private float chargeTimeElapsed = 0;
        void CheckTransition_Charging()
        {
            //if I hit something, rest.
            //if I finish my charge distance, rest. 
            float movedDistance = (this.transform.position - chargeStartPosition).magnitude;
            if (collidedWithSomething || movedDistance > chargeDistance)
            {
                Start_Resting();
            }
            if (chargeTimeElapsed > chargeTimeout)
                Start_Resting();
        }

        void Start_Charging()
        {
            currentState = ChargerState.Charging;
            chargeStartPosition = this.transform.position;
            this.movement.speed = moveSpeedWhileCharging;
            this.movement.LookSpeed = 0f;
            var overshootVector =
                (target.position - chargeStartPosition).normalized * chargeDistanceOvershootPercentage;
            this.TargetPosition = target.position + overshootVector;
            
            chargeTimeElapsed = 0;
        }
        
        void Update_Charging()
        {
            chargeTimeElapsed += Time.deltaTime;
        }
        #endregion

        #region Moving
        void CheckTransition_Moving()
        {
            //if I'm close enough to melee, do so.
            //if I'm close enough to charge, do that instead
            var toTargetDistance = (transform.position - target.position).magnitude;
            if (toTargetDistance <= meleeDistance) Start_Melee();
            else if (toTargetDistance <= chargeDistance * (1-chargeDistanceOvershootPercentage)) Start_PowerUp();
        }

        private Transform target;
        private NavMeshAgent navMeshAgent;
        void Start_Moving()
        {
            currentState = ChargerState.Moving;
            target = GameObject.Find("PlayerRoot").GetComponent<Transform>();
            
            this.movement.speed = regularLookSpeed;
            this.movement.LookSpeed = regularMoveSpeed;

        }
        
        void Update_Moving(bool rotateOnly = false)
        {
            var closeEnoughVector = (transform.position - target.position).normalized * meleeDistance;
            this.navMeshAgent.SetDestination(closeEnoughVector + target.position);
            this.TargetPosition = navMeshAgent.nextPosition;
            this.TargetPosition = new Vector3(TargetPosition.x, target.position.y, TargetPosition.z);
            if (rotateOnly)
            {
                this.TargetPosition = (TargetPosition - this.transform.position).normalized * 0.001f; //very short vector
            }
            Debug.DrawLine(transform.position, TargetPosition);
        }
        #endregion
        
        #region Resting
        void CheckTransition_Resting()
        {
            if (restRemainingTime <= 0.0f)
            {
                Start_Moving();
            }
        }

        private float restRemainingTime;
        void Start_Resting()
        {
            currentState = ChargerState.Resting;
            restRemainingTime = cooldownTime;
            
            this.movement.speed = 0;
            this.movement.LookSpeed = 0;
        }
        void Update_Resting()
        {
            restRemainingTime -= Time.deltaTime;
        }
        #endregion

        #region Melee
        void CheckTransition_Melee()
        {
            var toTargetDistance = (transform.position - target.position).magnitude;
            if (meleeDistance < toTargetDistance)
            {
                Start_Moving();
            }
        }

        void Start_Melee()
        {
            currentState = ChargerState.Melee;
            //Don't do anything here, let's reuse the melee attack script. We can transition out of this once we're no longer in the radius
        }
        
        void Update_Melee()
        {
            Update_Moving(true);
            //Don't do anything here, let's reuse the melee attack script. We can transition out of this once we're no longer in the radius
        }
        #endregion
        
        #region PowerUp

        private float powerUpElapsed;
        void CheckTransition_PowerUp()
        {
            if (powerUpElapsed > chargeUpTime)
            {
                Start_Charging();
            }
        }

        void Start_PowerUp()
        {
            currentState = ChargerState.PowerUp;
            //TODO trigger charge up anim
            powerUpElapsed = 0.0f;

            this.movement.speed = 0;
            this.movement.LookSpeed = lookSpeedWhilePowerUp;
        }
        
        void Update_PowerUp()
        {
            powerUpElapsed += Time.deltaTime;
            this.TargetPosition = target.position;
        }
        #endregion
    }

}

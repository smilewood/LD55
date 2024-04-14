using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace LD55
{
    public class MeleeAttack : MonoBehaviour
    {
        public string TypeToDamage;

        public float MeleeRange;

        public int DamagePerHit;

        public float CooldownSeconds;
        private float cooldownTimer;
        public UnityEvent OnDealDamage;

        // Start is called before the first frame update
        void Start()
        {
            cooldownTimer = 0;
            
        }

        // Update is called once per frame
        void Update()
        {
            cooldownTimer = Math.Max(0.0f, cooldownTimer - Time.deltaTime);//timer goes down to zero

            if (cooldownTimer > 0) return; // don't do anything unless I have someone to attack
            
            //attack all within range
            Vector3 myLocation = this.transform.position;
            bool attacked = false;
            foreach (var gameObj in GameObject.FindGameObjectsWithTag(TypeToDamage))
            {
                var distanceFromMe = (gameObj.transform.position - myLocation).magnitude;
                if (distanceFromMe < MeleeRange)
                {
                    if(gameObj.TryGetComponent(out HealthTracker hp))
                    {
                        attacked = true;
                        hp.ApplyDamage(DamagePerHit);
                    }
                    OnDealDamage?.Invoke();
                }
            }

            if (attacked) cooldownTimer = CooldownSeconds;
        }
    }
}
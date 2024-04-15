using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;

namespace LD55
{
    public class ProjectileAttack : MonoBehaviour
    {
        public float projectilesPerSecond;
        private float cooldownSec; // inverse of projectilesPerSecond
        private float cooldownTimeRemaining;
        public GameObject projectileToSpawn;
        public SoundOrMusic projectileFireSound;
        public float projectileSpeed;
        private SpriteParalax sprite;
        
        public float AttackRangeMin = 0f; //leave at zero if no need for switching between melee and projectiles
        public float AttackRangeMax;

        /// <summary>
        /// Attack range and lifetime are linked!
        /// </summary>
        /// 
        private float ProjectileLifetime => projectileSpeed == 0 ? 0 : AttackRangeMax / projectileSpeed;

        public float ProjectileLifetimeOverride = 0.0f;
        public string TypeToAttack;
        
        // Start is called before the first frame update
        void Start()
        {
            cooldownSec = 1.0f / projectilesPerSecond;
            cooldownTimeRemaining = 0f;
            sprite = gameObject.GetComponentInChildren<SpriteParalax>();
        }

        // Update is called once per frame
        void Update()
        {
            
            var target = FindClosestObjectWithTag(TypeToAttack, TypeToAttack == "Player");
            if (target is null) 
            {
                //Debug.Log("No target right now.");
                return;
            }
            var toTargetVector = target.transform.position - this.transform.position;
            Debug.DrawLine(this.transform.position, target.transform.position);
            if (toTargetVector.magnitude > AttackRangeMax || toTargetVector.magnitude < AttackRangeMin)
            {
                //Debug.Log("Target(s) out of range!");
                return;
            }

            //cooldown
            cooldownTimeRemaining = Math.Max(0f, cooldownTimeRemaining - Time.deltaTime);
            if (cooldownTimeRemaining > 0.0f) return;
            
            //we have a target, we have line of sight. let's shoot
            var rotation = Quaternion.LookRotation(toTargetVector, Vector3.up);
            
            var x = GameObject.Instantiate(projectileToSpawn, transform.position, rotation);
            x.GetComponent<Rigidbody>().velocity = 10.0f * toTargetVector.normalized;
            Destroy(x.gameObject, ProjectileLifetime + ProjectileLifetimeOverride);
            cooldownTimeRemaining = cooldownSec;
            SoundManager.GlobalSoundManager.PlaySound(projectileFireSound);
            sprite.SetAnimationTrigger("Attack");
        }

        [CanBeNull]
        GameObject FindClosestObjectWithTag(string tag, bool requireTag = true)
        {
            //attack the closest enemy that has the AttackMe script on it
            return GameObject.FindGameObjectsWithTag(tag)
                .Where(obj => !requireTag || obj.TryGetComponent<EnemyCanAttackMe>(out _))
                .OrderBy(obj => (obj.transform.position - this.transform.position).magnitude)
                .FirstOrDefault();
        }

    }
}

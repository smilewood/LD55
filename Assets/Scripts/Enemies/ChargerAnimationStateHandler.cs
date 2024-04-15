using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD55
{
    [RequireComponent(typeof(ChargerTracking))]
    public class ChargerAnimationStateHandler : MonoBehaviour
    {
        public SoundOrMusic ChargeSound;
        
        // Start is called before the first frame update
        private SpriteParalax sprite;
        private ChargerTracking stateTracking;

        private ChargerTracking.ChargerState currentState;
        void Start()
        {
            sprite = GetComponent<SpriteParalax>();
            stateTracking = GetComponent<ChargerTracking>();
        }

        // Update is called once per frame
        void Update()
        {
            if (currentState != stateTracking.currentState)
            {
                switch (stateTracking.currentState)
                {
                    case ChargerTracking.ChargerState.PowerUp:
                    //case ChargerTracking.ChargerState.Charging:
                        sprite.SetAnimationTrigger("Charge");
                        SoundManager.GlobalSoundManager.PlaySound(SoundOrMusic.FlightInsectLoop);
                        break;
                    default:
                    case ChargerTracking.ChargerState.Resting:
                    case ChargerTracking.ChargerState.Moving:
                        sprite.SetAnimationTrigger("Walk");
                        break;
                    //handled by melee attack code
                    //case ChargerTracking.ChargerState.Melee:
                    //    sprite.SetAnimationTrigger("Attack");
                }
                
                
                
            }
        }
    }
}

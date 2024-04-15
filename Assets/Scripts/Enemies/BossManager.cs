using UnityEngine;
using UnityEngine.AI;

namespace LD55
{
    [RequireComponent(typeof(HealthTracker))]
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(Rigidbody))]
    public class BossManager : MonoBehaviour
    {
        public HealthTracker healthTracker;
        public NavMeshAgent agent;
        public Rigidbody rb;

        void Start()
        {
            SoundManager.GlobalSoundManager.AudioState = AudioState.InGameBoss;
		}
    }
}

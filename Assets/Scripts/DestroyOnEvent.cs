using UnityEngine;
using UnityEngine.Events;

namespace LD55
{
   public class DestroyOnEvents : MonoBehaviour
   {
      public float ManaOnDestroy;
      public UnityEvent OnDeath;
      public bool ActuallyDestroyMe = true;
      [SerializeField] private bool SpawnBlood = true;
      private BloodSplatter BloodSplatterManager;

      private void Start()
      {
         if (SpawnBlood)
         {
            BloodSplatterManager = FindObjectOfType<BloodSplatter>();
            if (BloodSplatterManager == null)
            {
               Debug.LogError("BloodSplatterManager not found");
            }
         }
      }

      public void DestroyMe()
      {
         OnDeath?.Invoke();

         if (ManaOnDestroy > 0)
         {
            PlayerMana.ManaChangeEvent.Invoke(ManaOnDestroy);
         }

         if (SpawnBlood)
         {
            BloodSplatterManager.SpawnBlood(transform.position);
         }
         else if (ManaOnDestroy > 0)
         {
            // not an enemy so it's a mana crystal
            SoundManager.GlobalSoundManager.PlaySound(SoundOrMusic.ManaPickup);
         }

         if (ActuallyDestroyMe)
         {
            Destroy(this.gameObject);
         }

      }

      public void OnZeroHealth(HealthTracker hp, int _)
      {
         if (hp.CurrentHealth <= 0)
         {
            DestroyMe();
         }
      }
   }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace LD55
{
   /// <summary>
   /// Notification that health changed, and by how much
   /// </summary>
   [Serializable]
   public class HealthEvent : UnityEvent<HealthTracker, int> {}

   public class HealthTracker: MonoBehaviour, IStatusBarCheckable
   {
      public int MaxHealth;
      public int CurrentHealth
      {
         get; private set;
      }
      public HealthEvent OnHealthChange;

      public float CurrentPercent
      {
         get
         {
            return (float)CurrentHealth / (float)MaxHealth;
         }
      }

      private void Start()
      {
         CurrentHealth = MaxHealth;
	  }

		public void ApplyDamage(int damageAmount)
		{
			int previousHealth = CurrentHealth;
			CurrentHealth = Math.Clamp(CurrentHealth - damageAmount, 0, MaxHealth);
			if (CurrentHealth == previousHealth) return; //no actual change
			OnHealthChange?.Invoke(this, damageAmount);
			Debug.Log($"{gameObject.name} took {damageAmount} points of damage");

			if (gameObject.tag == "Enemy")
			{
				if (CurrentHealth <= 0)
				{
					SoundManager.GlobalSoundManager.PlaySound(SoundOrMusic.DefeatEnemy);
				}
				else
				{
					if (damageAmount > 0)
					{
						SoundManager.GlobalSoundManager.PlaySound(SoundOrMusic.DamageEnemy);
					}
					else if (damageAmount < 0)
					{
						SoundManager.GlobalSoundManager.PlaySound(SoundOrMusic.HealEnemy);
					}
				}
			}
			else if (gameObject.name == "PlayerRoot")
			{
				if (CurrentHealth <= 0)
				{
					SoundManager.GlobalSoundManager.PlaySound(SoundOrMusic.DefeatPlayer);
					SoundManager.GlobalSoundManager.PlaySound(SoundOrMusic.VoiceDefeat);
				}
				else
				{
					if (damageAmount > 0)
					{
						SoundManager.GlobalSoundManager.PlaySound(SoundOrMusic.DamagePlayer);
					}
					else if (damageAmount < 0)
					{
						SoundManager.GlobalSoundManager.PlaySound(SoundOrMusic.HealPlayer);
					}
				}
			}
			else if (gameObject.tag == "Seeker" || gameObject.name.ToLower().Contains("wasp"))
			{
				if (CurrentHealth <= 0)
				{
					SoundManager.GlobalSoundManager.PlaySound(SoundOrMusic.DefeatSummonedAlly);
				}
			}
			else if (gameObject.tag == "Boss")
			{
				float healthFraction = CurrentHealth / MaxHealth;
				if (healthFraction <= 0)
				{
					SoundManager.GlobalSoundManager.PlaySound(SoundOrMusic.VoiceVictory);
				}
				else if (healthFraction <= 0.33f)
				{
					SoundManager.GlobalSoundManager.PlaySound(SoundOrMusic.VoiceBoss1);
				}
				else if (healthFraction <= 0.67f)
				{
					SoundManager.GlobalSoundManager.PlaySound(SoundOrMusic.VoiceBoss2);
				}
				else
				{
					if (damageAmount > 0)
					{
						SoundManager.GlobalSoundManager.PlaySound(SoundOrMusic.DamageEnemy);
					}
					else if (damageAmount < 0)
					{
						SoundManager.GlobalSoundManager.PlaySound(SoundOrMusic.HealEnemy);
					}
				}
			}
		}
   }
}

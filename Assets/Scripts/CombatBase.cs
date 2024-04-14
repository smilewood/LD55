using UnityEngine;
using UnityEngine.Events;

namespace LD55
{
    public enum CombatantType
    { 
        Player,
		Ally,
		AllyProjectile,
        Enemy,
		EnemyProjectile
    }

	[RequireComponent(typeof(Rigidbody))]
	[RequireComponent(typeof(HealthTracker))]
	public abstract class CombatBase : MonoBehaviour
    {
        public CombatantType CombatantType;
		public UnityEvent OnDealDamage;
		public UnityEvent OnTakeDamage;
		public UnityEvent OnDeath;

		public int DamageAmount;
		public float ManaOnKill;

		private void OnCollisionEnter(Collision collision)
		{
			if (collision.gameObject.TryGetComponent(out CombatBase combatant))
			{
				var hp = collision.gameObject.GetComponent<HealthTracker>();
				hp.ApplyDamage(DamageAmount);
				OnDealDamage?.Invoke();

				// If this enemy didn't die by walking into a player, reward the player with mana!
				if (hp.CurrentHealth <= 0)
				{
					if (CombatantType != CombatantType.Player)
					{
						PlayerMana.ManaChangeEvent.Invoke(ManaOnKill);
					}

					OnDeath?.Invoke();
				}
			}
		}
	}
}

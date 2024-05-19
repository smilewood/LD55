using UnityEngine;
using UnityEngine.EventSystems;

namespace LD55
{
   public class PlayerMouseActions : MonoBehaviour
   {
      public Spawnable ActiveSpawnable;
      public Transform SummonParent;
      public float distanceToSpawnFromPlayer;
	   public bool CanISummon = true;

      private float cooldown;

      // Update is called once per frame
      void Update()
      {
         if(cooldown >= 0)
         {
            cooldown -= Time.deltaTime;
         }
         if (CanISummon &&
            cooldown <= 0 && 
            !ActiveSpawnable.Locked &&
            Input.GetButton("Fire1") && 
            !EventSystem.current.IsPointerOverGameObject() &&
            PlayerMana.Instance.CurrentMana > ActiveSpawnable.ManaCost)
         {
            Spawn();
         }

      }

      public void Spawn()
      {
         cooldown = ActiveSpawnable.Cooldown;


         PlayerMana.ManaChangeEvent.Invoke(-ActiveSpawnable.ManaCost);

         var angle = Random.Range(-45f, 45f);
         var spawnAngleUnitVector = Quaternion.Euler(0, angle, 0) * (GetMousePositionInWorld() - transform.position).normalized;
         var spawnVector = spawnAngleUnitVector * distanceToSpawnFromPlayer + this.transform.position;
         spawnVector.y = this.transform.position.y;
         Instantiate(ActiveSpawnable.SpawnPrefab, spawnVector, Quaternion.AngleAxis(angle, Vector3.up), SummonParent);
         SoundManager.GlobalSoundManager.PlaySound(SoundOrMusic.SummonAlly);

      }

      public static Vector3 GetMousePositionInWorld()
      {
         //get mouse position
         Vector3 mousePos = Input.mousePosition;
         Ray cameraRay = Camera.main.ScreenPointToRay(mousePos);
         if (Physics.Raycast(cameraRay, out RaycastHit hitData))
         {
            return hitData.point;
         }
         else
         {
            return Vector3.zero;
         }
      }

   }
}

using UnityEngine;
using UnityEngine.EventSystems;

namespace LD55
{
   public class PlayerMouseActions : MonoBehaviour
   {
      public Spawnable ActiveSpawnable;
      public Transform SummonParent;
      public float distanceToSpawnFromPlayer;

      private float cooldown;

      // Update is called once per frame
      void Update()
      {
         if(cooldown >= 0)
         {
            cooldown -= Time.deltaTime;
         }
         if (cooldown <= 0 && Input.GetButton("Fire1") && !EventSystem.current.IsPointerOverGameObject()) //left click
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

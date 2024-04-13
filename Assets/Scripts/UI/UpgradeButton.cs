using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace LD55
{
   public class UpgradeButton : MonoBehaviour
   {
      public static UnityEvent OnUpgradeComplete
      {
         get
         {
            _onUpgradeComplete ??= new UnityEvent();
            return _onUpgradeComplete;
         }
      }
      private static UnityEvent _onUpgradeComplete;

      public GameObject newPrefab;

      public GameObject NextTier;

      private Button button;
      // Start is called before the first frame update
      void Start()
      {
         button = GetComponent<Button>();
         button.onClick.AddListener(ApplyUpgrade);
      }

      private void ApplyUpgrade()
      {
         if(NextTier != null)
         {
            NextTier.GetComponent<Button>().interactable = true;
         }
         button.interactable = false;

         //TODO: Also have to set the spawnning prefab to the new one

         OnUpgradeComplete.Invoke();
      }
   }
}

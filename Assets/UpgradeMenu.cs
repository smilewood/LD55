using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD55
{
   public class UpgradeMenu : MonoBehaviour
   {
      // Start is called before the first frame update
      void Start()
      {
         UpgradeButton.OnUpgradeComplete.AddListener(HideMenu);
      }

      private void HideMenu()
      {
         this.gameObject.SetActive(false);
      }

   }
}

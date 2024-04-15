using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD55
{
   public class ShowMenuOnEvent : MonoBehaviour
   {
      public bool AlsoPause;
      public void ShowMenu(string menu)
      {
         MenuFunctions.PauseGame(false);
         MenuFunctions.Instance.ShowMenu(menu);
      }
   }
}

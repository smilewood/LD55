using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LD55
{
   public class StatusBarUpdater : MonoBehaviour
   {
      public MonoBehaviour Target;
      private IStatusBarCheckable status;
      public Image bar;

      private void Start()
      {
         if(Target is IStatusBarCheckable)
         {
            status = Target as IStatusBarCheckable;
         }
         else
         {
            Debug.LogError("Status target is not the right type");
         }

         
      }

      // Update is called once per frame
      void Update()
      {
         bar.fillAmount = status?.CurrentPercent ?? 0;
      }
   }
}

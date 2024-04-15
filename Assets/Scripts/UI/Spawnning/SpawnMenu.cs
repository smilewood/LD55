using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LD55
{
   public class SpawnMenu : MonoBehaviour
   {
      public List<Spawnable> SummonTypes;
      private int count { get { return SummonTypes.Count; } }
      private PlayerMouseActions player;

      private int currentSelection = 1;

      public Image PrevImage, CurImage, NextImage;

    

      private void Start()
      {
         player = GameObject.Find("PlayerRoot").GetComponent<PlayerMouseActions>();
         ChangeSelection(0);
      }

      private void Update()
      {
         if (Input.GetKeyDown(KeyCode.Q))
         {
            ChangeSelection(-1);
         }
         if (Input.GetKeyDown(KeyCode.E))
         {
            ChangeSelection(1);
         }
      }

      public void ChangeSelection(int scroll)
      {
         currentSelection = CycleUpDown(currentSelection, count, scroll);

         PrevImage.sprite = SummonTypes[CycleUpDown(currentSelection, count, -1)].image;
         CurImage.sprite = SummonTypes[currentSelection].image;
         NextImage.sprite = SummonTypes[CycleUpDown(currentSelection, count, 1)].image;

         player.ActiveSpawnable = SummonTypes[currentSelection];

         static int CycleUpDown(int start, int max, int delta)
         {
            int result = start + delta;
            return result < 0 ? max-1 : result % max;
         }
      }

      public void UpgradeSummon(int index, Spawnable newSpawnable)
      {
         if (index < SummonTypes.Count)
         {
            SummonTypes[index] = newSpawnable;
         }
         if(index == SummonTypes.Count)
         {
            SummonTypes.Add(newSpawnable);
         }
         ChangeSelection(0);
      }

   }
}

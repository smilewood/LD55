using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD55
{
   public class Spawnable : MonoBehaviour
   {
      public bool Locked;
      public GameObject SpawnPrefab;
      public float ManaCost;
      public float ManaLock;
      public float Cooldown;

      public Sprite image;
   }
}

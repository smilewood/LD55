using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace LD55
{
   public class SpriteParalax : MonoBehaviour
   {
      public GameObject LayerPrefab;
      public int LayerCount;
      public RuntimeAnimatorController Animator;
      public float LayerSkew;

      private List<Transform> layerObjects;
      private Camera mainCam;
      private AnimatorOverrideController myAnimator;

      public bool SkipDelay = false;

      // Start is called before the first frame update
      void Start()
      {
         mainCam = Camera.main;
         layerObjects = new List<Transform>();

         float frameDelay = Random.Range(0, 5) * .1f;
         frameDelay = SkipDelay ? 0 : frameDelay;

         myAnimator = new AnimatorOverrideController(Animator);
         for (int i = 0; i < LayerCount; ++i)
         {
            GameObject l = Instantiate(LayerPrefab, this.transform);
            //Set animation
            l.GetComponent<SpriteRenderer>().sortingOrder = i;
            Animator a = l.GetComponent<Animator>();

            a.runtimeAnimatorController = myAnimator;
            a.SetInteger("StartLayer", i);
            StartCoroutine(DelayAnimationStart(a, frameDelay));
            layerObjects.Add(l.transform);
         }
      }

      private IEnumerator DelayAnimationStart(Animator a, float delay)
      {
         yield return new WaitForSeconds(delay);
         a.SetTrigger("Start");
      }

      // Update is called once per frame
      void Update()
      {
         //find dist and bearing from center

         Vector3 screenPos = mainCam.WorldToViewportPoint(this.transform.position);

         Vector3 toCenter = screenPos - new Vector3(.5f, .5f, 0);

         float distToCenter = toCenter.magnitude;
         Vector3 centerDirection = toCenter.normalized;
         centerDirection = new Vector3(centerDirection.x, 0, centerDirection.y);


         //offset each layer based on dist
         float layerOffset = 0;
         foreach (Transform t in layerObjects)
         {
            t.transform.position = this.transform.position + (distToCenter * layerOffset * centerDirection);
            layerOffset += LayerSkew;
         }
      }

      public void SetMovingBool(bool state)
      {
         foreach (Animator a in GetComponentsInChildren<Animator>())
         {
            //GetComponentInChildren<Animator>().SetTrigger("Attack");
            a.SetBool("Moving", state);
         }
      }
      
      public void SetAttackTrigger()
      {
         foreach (Animator a in GetComponentsInChildren<Animator>())
         {
            a.SetTrigger("Attack");
         }
      }

   }
}

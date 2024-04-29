using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace LD55
{
   public class StartVolumeSlider : MonoBehaviour
   {
      public AudioMixer mixer;
      public string Setting;
      
      // Start is called before the first frame update
      void Start()
      {
         Slider slider = this.GetComponent<Slider>();

         mixer.GetFloat(Setting, out float value);
         slider.value = Mathf.Pow(10, value / 20);
      }

   }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD55
{
    public class BloodSplatter : MonoBehaviour
    {

        private static BloodSplatter instance;

        [SerializeField] 
        private GameObject bloodSplatter;
    
        private GameObject[] splatPool = new GameObject[10];
        private int counter = 0;

        private void Start()
        {
            for(int i=0; i<10; i++)
            {
                splatPool[i] = Instantiate(bloodSplatter);
                splatPool[i].SetActive(false);
            }
        }
        public void SpawnBlood(Vector3 location)
        {
            splatPool[counter].SetActive(true);
            splatPool[counter].transform.position = location;
            splatPool[counter].transform.rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
            float scale = Random.Range(.1f, .3f);
            splatPool[counter].transform.localScale = new Vector3(scale, 1f, scale);
            Increment();
        }

        public void ClearBlood()
        {
            foreach(GameObject splat in splatPool)
            {
                splat.SetActive(false);
            }
        }

        private void Increment()
        {
            if (counter < 9)
            {
                counter++;
            }
            else
            {
                counter = 0;
            }
        }

            //transform.Rotate(0, Random.Range(0, 360), 0);


    }
}

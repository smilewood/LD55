using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD55
{
    public class manaCrystalSpin : MonoBehaviour
    {
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            transform.Rotate(0, .2f, 0);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD55
{
    public class Lifetime : MonoBehaviour
    {
        public float lifetimeSeconds;
        
        
        // Start is called before the first frame update
        void Start()
        {
            Destroy(this.gameObject ,lifetimeSeconds);
        }
    }
}

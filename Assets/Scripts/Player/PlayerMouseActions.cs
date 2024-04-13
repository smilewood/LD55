using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace LD55
{
    public class PlayerMouseActions : MonoBehaviour
    {
        public GameObject leftClickSpawn;
        public float distanceToSpawnFromPlayer;
        
        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0)) //left click
            {
                Debug.Log("Mouse Left Clicked");
                Spawn();
            }
            
        }
        
        public void Spawn()
        {
            var angle = UnityEngine.Random.Range(-45f,45f);
            var spawnAngleUnitVector = UnityEngine.Quaternion.Euler(0,angle,0) * (GetMousePositionInWorld() - transform.position).normalized;
            var spawnVector = spawnAngleUnitVector * distanceToSpawnFromPlayer + this.transform.position;
            Instantiate(leftClickSpawn, spawnVector, quaternion.AxisAngle(Vector3.up,angle));
        }

        public static Vector3 GetMousePositionInWorld()
        {
            var targetPos = Vector3.zero;
            //get mouse position
            var mousePos = Input.mousePosition;
            var cameraRay = Camera.main.ScreenPointToRay(mousePos);
            if (Physics.Raycast(cameraRay, out RaycastHit hitData))
            {
                targetPos = hitData.point;
            }

            return targetPos;
        }

    }
}

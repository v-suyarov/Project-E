using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;
using System;

namespace MoreMountains.CorgiEngine
{
    public class GlowwormFollow : MonoBehaviour
    {
        
        public CorgiController controller;
        public Transform DefaultPosition;
        public float offset_x=2;
        private float max_speed=15;
        public float speed = 10f;
        //dsada
        public float error = 0.1f;

        private Vector3 def;
        private Vector3 light;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            float deviation = controller.Speed.x / max_speed;

            Vector3 glowworm_local_pos = controller.transform.InverseTransformPoint(transform.position);
            Vector3 default_local_pos = controller.transform.InverseTransformPoint(DefaultPosition.position);
            if (Math.Abs(deviation) > 1)
                deviation = deviation / Mathf.Abs(deviation);

            if (Mathf.Abs(deviation) <= 0.01)
            {
                if (Mathf.Abs(glowworm_local_pos.x) >= error)
                {
                    transform.Translate((DefaultPosition.position - transform.position).normalized * speed*Time.deltaTime, Space.World);
                }
            }
            else if (deviation > 0.01)
            {
                if (Mathf.Abs(glowworm_local_pos.x - offset_x) >= error)
                {
                    Vector3 right_defaultPosition = DefaultPosition.position;
                    right_defaultPosition.x += offset_x;

                    transform.Translate((right_defaultPosition - transform.position).normalized * speed * Time.deltaTime * Mathf.Abs(deviation), Space.World);
                }
            }
            else if (deviation < 0.01)
            {
                if (Mathf.Abs(glowworm_local_pos.x + offset_x) >= error)
                {
                  
                    Vector3 right_defaultPosition = DefaultPosition.position;
                    right_defaultPosition.x -= offset_x;

                    transform.Translate((right_defaultPosition - transform.position).normalized * speed * Time.deltaTime * Mathf.Abs(deviation), Space.World);
                }
            }
            Debug.Log(Mathf.Abs(default_local_pos.x + glowworm_local_pos.x + offset_x));
            def = DefaultPosition.position;
            light = transform.position;
            // Debug.Log(();
        }
    }

        
    
}

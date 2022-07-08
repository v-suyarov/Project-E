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
        public float offset_x = 2;
        public float offset_y = 2;
        public float period = 2;

        private float max_speed = 15;
        public float speed = 10f;
        //dsada
        public float error = 0.1f;

        private Vector3 def;
        private Vector3 light;
        // Start is called before the first frame update
        void Start()
        {
            Vector3 graf = Vector3.zero;

            for (float x = 0; x < 8; x += 0.1f)
            {
                graf.x = x;
                graf.y = (float)(Math.Sin(x * Math.PI / 8) + 1.5f);
                
                Debug.DrawLine(graf, graf+Vector3.down, Color.red, 10f);

            }


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
                    Vector3 next_position = Vector3.zero;
                 
                    next_position.y = (float)( Math.Sin(glowworm_local_pos.x * Math.PI / 8+glowworm_local_pos.x) + 1.5f);

                    Vector3 direction = default_local_pos - glowworm_local_pos;
                    direction.y = next_position.y - glowworm_local_pos.y;
                   

                    if (Mathf.Abs(glowworm_local_pos.x) <= 0.01)
                    {

                    }

                    //transform.Translate(direction.normalized * speed * Time.deltaTime, Space.Self);
                    
                    transform.position = Vector2.Lerp(transform.position, DefaultPosition.position, Time.deltaTime);



                    Vector3 temp = transform.position;
                    
                    Vector3 temp1 = temp;
                    temp1.x += 0.1f;
                    Debug.DrawLine(temp, temp1, Color.red, 10f);


                    //Debug.Log(pos_y.y.ToString()+": Y");
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

            def = DefaultPosition.position;
            light = transform.position;
            // Debug.Log(();

            
        }
    }
    
}
    public class Gizmo : MonoBehaviour
    {
   
    void OnDrawGizmos() //Встроенный метод,необходим для работы с Gizmos объектами и их отрисовки
    {
        Gizmos.color = Color.green;

        
        Gizmos.DrawRay(transform.position,Vector3.right);
    }

}
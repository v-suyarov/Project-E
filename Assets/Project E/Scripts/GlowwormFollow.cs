using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;
using System;

namespace MoreMountains.CorgiEngine
{

    
   
    public class GlowwormFollow : MonoBehaviour
    {
        public enum FlightBehavior
        {
           Sin,
           Cos,
           Negative_sin,
           Negative_cos,
           
           Castom_function_1,
                    
        }
        [Header("Flight Behavior")]
        [Tooltip("Насколько далеко cветлячок будет отлетать от персонажа")]
        [SerializeField] private float distanse = 4f;
        [Tooltip("Выбор функции влияет на траекторию полета")]
        [SerializeField] private FlightBehavior flightBehavior;
        [Tooltip("Переод функции")]
        [SerializeField] float  period = 2;
        [Tooltip("Умножает функцию на указанное значение")]
        [SerializeField] private float multiply =1f;
        [Tooltip("Скорость полета")]
        [SerializeField] private float speed;
        //[Tooltip("Сдвиг светлячка в состоянии покоя")]
        private float offset = 0.1f;
        private GameObject player;       
        private float elapsedTime = 0f;
        private float progress =  0.5f;
        private float timeRest = 0.0f;
        private Vector2 defaultLocalPos= Vector2.zero;
        //ошабка при сравнении цифр с плавающей точкой
        private float error = 1f;


        private void Start()
        {   //запоминаю дефолтную позицию
            defaultLocalPos = transform.localPosition;
            player = transform.root.gameObject;
            //для более удобной настройки
           
        }
        private void Update()
        {

            Debug.Log(player.GetComponent<Character>().IsFacingRight);
            if (player.GetComponent<CorgiController>().Speed.x>error&&progress<1)
            {
                   
                    progress += Time.deltaTime / 8 * speed;
                    Vector2 nextPos = Vector2.Lerp(new Vector2(-distanse, 0), new Vector2(distanse, 0), progress);
                    nextPos.y = GetY_ByFunction(nextPos.x, flightBehavior);
                    Debug.DrawLine(transform.position, transform.position + new Vector3(0.1f, 0, 0), Color.red, 5f);
                    transform.localPosition = nextPos;
                    if (progress > 1)
                    {
                        progress = 1;
                        
                    }
                
            }
            if(player.GetComponent<CorgiController>().Speed.x < -error && progress > 0)
            {

                    progress -= Time.deltaTime / 8 * speed;
                    Vector2 nextPos = Vector2.Lerp(new Vector2(-distanse, 0), new Vector2(distanse, 0), progress);
                    nextPos.y = GetY_ByFunction(nextPos.x, flightBehavior);
                    Debug.DrawLine(transform.position, transform.position + new Vector3(0.1f, 0, 0), Color.red, 5f);
                    transform.localPosition = nextPos;
                    if (progress < 0)
                    {
                        progress = 0;

                    }              
            }
            if ( Math.Abs( player.GetComponent<CorgiController>().Speed.x) <= error  )
            {
                if (progress <0.5f-offset||progress>0.5f+offset)
                {
                    float prev_progress = progress;
                    if (progress > 0.5)
                    {

                        progress -= Time.deltaTime / 8 * speed;
                        if (player.GetComponent<Character>().IsFacingRight && progress < 0.5f + offset)
                        {
                            progress = prev_progress;
                        }
                    }
                    else
                    {
                        progress += Time.deltaTime / 8 * speed;
                        if (!player.GetComponent<Character>().IsFacingRight && progress > 0.5f - offset)
                        {
                            progress = prev_progress;
                        }

                    }
                    Vector2 nextPos = Vector2.Lerp(new Vector2(-distanse, 0), new Vector2(distanse, 0), progress);
                    nextPos.y = GetY_ByFunction(nextPos.x, flightBehavior);

                    Debug.DrawLine(transform.position, transform.position + new Vector3(0.1f, 0, 0), Color.red, 5f);
                    transform.localPosition = nextPos;
                    if (Math.Abs(progress - 0.5) < 0.02)
                    {
                        progress = 0.5f;

                    }
                }
                else
                {
                    if (player.GetComponent<Character>().IsFacingRight)
                    {
                        progress += Time.deltaTime / 8 * speed;
                    }
                    else
                    {
                        progress -= Time.deltaTime / 8 * speed;
                    }
                    Vector2 nextPos = Vector2.Lerp(new Vector2(-distanse, 0), new Vector2(distanse, 0), progress);
                    nextPos.y = GetY_ByFunction(nextPos.x, flightBehavior);

                    
                    transform.localPosition = nextPos;
                }
            }
            if (player.GetComponent<Character>().MovementState.CurrentState==CharacterStates.MovementStates.Idle)
            {

            }

           
        }

        public float GetY_ByFunction(float x,FlightBehavior function)
        { 
            x = Mathf.Abs(x);
            if(function==FlightBehavior.Sin)
            {
                return ((float)Math.Sin(x * Math.PI / period) *multiply)+defaultLocalPos.y;
            }
            else if(function == FlightBehavior.Cos)
            {

                return ((float)Math.Cos(x * Math.PI / period) * multiply) + defaultLocalPos.y;

            }
            else if (function == FlightBehavior.Negative_sin)
            {

                return -((float)Math.Sin(x * Math.PI / period) * multiply) + defaultLocalPos.y;

            }
            else if (function == FlightBehavior.Negative_cos)
            {

                return -((float)Math.Cos(x * Math.PI / period) * multiply) + defaultLocalPos.y+1*multiply;

            }
            else if (function == FlightBehavior.Castom_function_1)
            {
                //в итоге позволит завtршить движение на y=0, работает только при периоде = 1
                float normolize = 4.2549f / distanse;
                return ((float)Math.Sin(x*normolize * Math.PI / period - 7) * x*normolize / 10 * multiply) + defaultLocalPos.y;

            }

            return defaultLocalPos.y;
        }

    }
    

    public class Gizmo : MonoBehaviour
    {

        void OnDrawGizmos() //Встроенный метод,необходим для работы с Gizmos объектами и их отрисовки
        {
            Gizmos.color = Color.green;


            Gizmos.DrawRay(transform.position, Vector3.right);
        }
    }
}
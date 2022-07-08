using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Gizm : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;

        for (float x = 0; x < 8;)
        {
            Vector3 pos = Vector3.zero;

            pos.x = x;
            pos.y = (float)(2 * Math.Sin(x * Math.PI / 8) + 1.5f); ;
            Gizmos.DrawSphere(pos, 0.1f);
            x += 0.1f;
        }
    }
}

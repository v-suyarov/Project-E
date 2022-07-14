using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayScan : MonoBehaviour
{
	public LayerMask layerMask;
	private float distance = 1f;
    // Update is called once per frame
    void Update()
    {
		RaycastHit2D hit_left = Physics2D.Raycast(transform.localPosition, transform.TransformDirection(Vector2.down), distance, layerMask);
		RaycastHit2D hit_right = Physics2D.Raycast(transform.localPosition, transform.TransformDirection(Vector2.down), distance, layerMask);
		Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.left) * distance, Color.black);
		Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.right) * distance, Color.black);
		if (hit_left.collider != null)
		{
			Debug.Log("стена слева");
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollow : MonoBehaviour
{
	public GameObject player;
	List<Rigidbody2D> touchObjects = new List<Rigidbody2D>();
	private void Start()
	{
		for(int i = 0; i < 2; i++)
		{
			GameObject go = Instantiate(player);
			touchObjects.Add(go.GetComponent<Rigidbody2D>());
		}
	}

	Vector2 prevPos;
	public float speedMagnitude;
	private void FixedUpdate()
	{
		if (!HealthController.dead)
		{	
			if(Input.touchCount == 1)
			{

				touchObjects[0].gameObject.SetActive(true);
				MoveRBToPos(Input.GetTouch(0).position, touchObjects[0]);
				touchObjects[1].gameObject.SetActive(false);
			}
			else if (Input.touchCount > 1)
			{
				for (int i = 0; i < 2; i++)
				{
					print(Input.GetTouch(i).position);
					touchObjects[i].gameObject.SetActive(true);
					MoveRBToPos(Input.GetTouch(i).position, touchObjects[i]);
				}
			}		
			else
			{
				for (int i = 0; i < 2; i++)
				{

					touchObjects[i].gameObject.SetActive(false);
				}
			}
			
		
		}	
		void MoveRBToPos(Vector3 input, Rigidbody2D rbv)
		{
			Vector2 pos = Camera.main.ScreenToWorldPoint(input);
			if (rbv.position.y >= 2)
			{
				Vector2 pos2 = rbv.position;
				pos2.y = 2f;
				rbv.position = pos2;
				if (pos.y > 2f)
				{
					pos.y = 2f;
				}
			}
			rbv.MovePosition(pos);
		}
	}
}

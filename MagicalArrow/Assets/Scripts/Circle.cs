using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour
{
	public GameObject particle;
	public GameObject friendlyP;
	bool friendly = false;
	int hitCounter = 0;
	public float playerDmg = 0.1f;
	public float enemyDmg = 0.15f;
	private void Update()
	{
		if (HealthController.end)
		{
			GetComponent<Rigidbody2D>().velocity *= 0;
		}
		if(GetComponent<Rigidbody2D>().velocity.magnitude < 2f)
		{
			Instantiate(friendly ? friendlyP : particle, transform.position, transform.rotation);
			DetachTrail();
			Destroy(gameObject);
		}
	}
	private void OnBecameInvisible()
	{
		Destroy(gameObject);
	}
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (!friendly)
		{
			if (collision.collider.gameObject.layer == 8)
			{
				SoundSystem.PlaySound(transform.position, 1, true);
				friendly = true;
				gameObject.layer = 9;
				transform.GetChild(0).gameObject.layer = 9;
				GetComponentInChildren<SpriteRenderer>().color = Color.green;
				GetComponentInChildren<TrailRenderer>().startColor = Color.green;
				GetComponentInChildren<TrailRenderer>().endColor = Color.green;
				Instantiate(friendlyP, transform.position, transform.rotation);
				TimeShifter.Shift(0.1f, 0.8f);
			}
			if (collision.collider.gameObject.layer == 13)
			{
				SoundSystem.PlaySound(transform.position, 0);
				Instantiate(particle, transform.position, transform.rotation);
				HealthController.Damage(playerDmg + 0.03f);
				TimeShifter.Shift(0.15f, 0.3f);
				Destroy(gameObject);
			}
		}
		if (friendly)
		{
			if (collision.collider.gameObject.layer == 11)
			{
				Instantiate(friendlyP, transform.position, transform.rotation);
				TimeShifter.Shift();
				DetachTrail();
				SoundSystem.PlaySound(transform.position, 2);
				collision.collider.GetComponent<ObjectHP>().Damage(enemyDmg);
				Destroy(gameObject);
			}
			hitCounter++;
			if(hitCounter > 3)
			{
				Instantiate(friendlyP, transform.position, transform.rotation);
				if(GetComponentInChildren<TrailRenderer>() != null)
				{
					DetachTrail();
				}
				Destroy(gameObject);
			}
		}
	}
	private void OnCollisionExit2D(Collision2D collision)
	{
		Rigidbody2D rb = GetComponent<Rigidbody2D>();
		if (!friendly && collision.collider.gameObject.layer == 8)
		{
			rb.velocity = rb.velocity * 0.5f;
		}
		if (rb.velocity.magnitude > 15f)
		{
			rb.velocity = rb.velocity.normalized * 15f;
		}
	}
	void DetachTrail()
	{
		if(GetComponentInChildren<TrailRenderer>() != null)
		{
			GetComponentInChildren<TrailRenderer>().time = 0.2f;
			GetComponentInChildren<TrailRenderer>().autodestruct = true;
			GetComponentInChildren<TrailRenderer>().transform.parent = null;
		}

	}
}

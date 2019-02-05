using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Andr3wDown.Cooldown;

public class CircleShooter : MonoBehaviour
{
	public static List<CircleShooter> allShooters = new List<CircleShooter>();
	public Cooldown cooldown = new Cooldown(2, true);
	public GameObject circle;
	const float startZ = 180;
	public Transform shooter;
	float minz;
	float maxz;
	public float angle = 90;
	Quaternion desiredRot;
	private void OnEnable()
	{
		allShooters.Add(this);
	}
	private void OnDisable()
	{
		if (allShooters.Contains(this))
		{
			allShooters.Remove(this);
		}
	}
	private void Start()
	{
		minz = startZ - angle / 2;
		maxz = startZ + angle / 2;
		desiredRot = RandomRot();
	}
	void Update()
    {
		if (!HealthController.dead)
		{
			shooter.rotation = Quaternion.Slerp(shooter.rotation, desiredRot, 5f * Time.deltaTime);
			cooldown.CountDown(Time.deltaTime);
			if (cooldown.TriggerReady())
			{
				GameObject go = Instantiate(circle, shooter.position + shooter.up * 0.3f, Quaternion.identity);
				go.GetComponent<Rigidbody2D>().AddForce(shooter.up * Random.Range(6f,9f), ForceMode2D.Impulse);
				desiredRot = RandomRot();
			}
		}
	
    }
	Quaternion RandomRot()
	{
		float z = Random.Range(minz, maxz);
		return Quaternion.Euler(new Vector3(0, 0, z));
	}
}

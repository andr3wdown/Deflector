using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ObjectHP : MonoBehaviour
{
	public Image bar;
	float hp = 1;
	bool dead = false;
	public GameObject hpPrefab;
	[Range(0f, 1f)]
	public float hpa = 0.7f;
	private void Start()
	{
		GameObject hpParent = FindObjectOfType<HealthController>().gameObject;
		GameObject go = Instantiate(hpPrefab);
		go.transform.parent = hpParent.transform;
		go.transform.position = transform.position;
		bar = go.GetComponent<Image>();
		Color c = GetComponent<SpriteRenderer>().color;
		c.a = hpa;
		go.GetComponent<Image>().color = c;
	}
	public void Damage(float amount)
	{
		hp -= amount;
		if(hp <= 0)
		{
			hp = 0;
			if (!dead)
			{
				StartCoroutine(DeathSequence());
				dead = true;
			}
		}
		bar.fillAmount = hp / 1f;
	}
	IEnumerator DeathSequence()
	{
		float scale = 1;
		while(scale >= 0.01f)
		{
			TimeShifter.Shift(0.2f);
			scale -= 2f * Time.deltaTime;
			transform.localScale = new Vector3(scale, scale, scale);
			yield return null;
		}
		Destroy(gameObject);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthController : MonoBehaviour
{
	public static bool dead = false;
	public static bool won = false;
	private float hp = 1f;
	public GameObject endScreen;
	public Image endBackground;
	public Image bar;
	public Text endText;
	static HealthController instance;
	private void Start()
	{
		dead = false;
		won = false;
		instance = this;
		if (instance != this)
		{
			Destroy(gameObject);
		}
	}
	public static void Damage(float amount)
	{
		instance.hp -= amount;	
		if (instance.hp <= 0)
		{
			instance.hp = 0;
			if (!dead)
			{
				dead = true;
				instance.StartCoroutine(instance.DeathSequence());
			}	
		}
		instance.bar.fillAmount = instance.hp / 1f;
	}
	public static bool end
	{
		get
		{
			return won || dead;
		}
	}
	public static void Win()
	{
		if (!won)
		{
			instance.StartCoroutine(instance.WinSequence());
			won = true;
		}
	}
	IEnumerator WinSequence()
	{
		Color c = Color.black;
		c.a = 0f;
		while (c.a < 0.8f)
		{
			c.a += 2f * Time.deltaTime;
			endBackground.color = c;
			yield return null;
		}
		endText.color = Color.green;
		endText.text = "You Win!";
		endScreen.SetActive(true);
	}
	IEnumerator DeathSequence()
	{
		Color c = Color.black;
		c.a = 0f;
		while(c.a < 0.8f)
		{
			c.a += 2f * Time.deltaTime;
			endBackground.color = c;
			yield return null;
		}
		endText.color = Color.red;
		endText.text = "You Lose!";
		endScreen.SetActive(true);
	}
	public void Restart()
	{
		SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
	}
	public void MainMenu()
	{
		SceneManager.LoadSceneAsync(0);
	}
}

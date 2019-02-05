using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeShifter : MonoBehaviour
{
	static TimeShifter instance;
	float timeScale = 1;
	float camSize;
	private void Start()
	{
		camSize = Camera.main.orthographicSize;
		instance = this;
		if(instance != this)
		{
			Destroy(gameObject);
		}
	}
	void Update()
    {
		
		Time.timeScale = timeScale;
		timeScale = Mathf.Lerp(timeScale, 1, 5f * Time.unscaledDeltaTime);
		Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, camSize, 4f * Time.unscaledDeltaTime);
	}
	public static void Shift(float amt = 0.1f, float t = 0.5f)
	{
		instance.timeScale = t;
		Camera.main.orthographicSize = instance.camSize - amt;
	}
}

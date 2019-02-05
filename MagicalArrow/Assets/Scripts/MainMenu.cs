using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
	public Image panel;
	public Text text;
	public GameObject levelSelect;
	bool selectOpen = false;
	int maxLevelIndex = 7;
	static int levelIndex = 0;
	private void Start()
	{
		levelIndex = 0;
		text.text = "Level " + (levelIndex + 1).ToString();
	}
	AsyncOperation o;
	public void LoadLevel()
	{
		SceneManager.LoadScene(levelIndex + 1);	
	}
	public void QuitGame()
	{
		Application.Quit();
	}
	public void LevelSelect(bool open)
	{
		if(selectOpen == open)
		{

		}
		else
		{
			
			StartCoroutine(LevelSelectMenu(open));
			selectOpen = open;
		}
	}
	IEnumerator LevelSelectMenu(bool open)
	{
		if (open ? !selectOpen : selectOpen)
		{
			Color c = panel.color;
			if (open)
			{
				while(c.a < 0.8f)
				{
					c.a += 2f * Time.deltaTime;
					panel.color = c;
					yield return null;
				}
				levelSelect.SetActive(open);

			}
			else
			{
				levelSelect.SetActive(open);
				while (c.a > 0f)
				{
					c.a -= 2f * Time.deltaTime;
					panel.color = c;
					yield return null;
				}
			}
		}
	
	}
	public void LevelIndex(int amt)
	{
		levelIndex += amt;
		if(levelIndex > maxLevelIndex)
		{
			levelIndex = 0;
		}
		if(levelIndex < 0)
		{
			levelIndex = maxLevelIndex;
		}
		text.text = "Level " + (levelIndex + 1).ToString();
	}
	private void OnDestroy()
	{
		levelIndex = 0;
	}
}

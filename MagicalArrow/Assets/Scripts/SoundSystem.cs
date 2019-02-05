using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSystem : MonoBehaviour
{
	const int STARTING_SPEAKERS = 3;
	static List<GameObject> sourcePool = new List<GameObject>();
	static SoundSystem instance;
	public AudioClip[] clips;
	private void Start()
	{
		instance = this;
		if(instance != this)
		{
			Destroy(gameObject);
		}
		GenerateSources();
	}
	void GenerateSources()
	{
		sourcePool = new List<GameObject>();
		sourcePool.Clear();

		for(int i = 0; i < STARTING_SPEAKERS; i++)
		{
			GameObject go = new GameObject();
			go.transform.parent = transform;
			go.AddComponent<AudioSource>();
			go.AddComponent<Speaker>();
			sourcePool.Add(go);
			go.SetActive(false);
		}
	}
	AudioSource GetFreeSpeaker()
	{
		for(int i = 0; i < sourcePool.Count; i++)
		{
			if (!sourcePool[i].activeInHierarchy)
			{
				return sourcePool[i].GetComponent<AudioSource>();
			}
		}

		GameObject go = new GameObject();
		go.transform.parent = transform;
		AudioSource s = go.AddComponent<AudioSource>();
		sourcePool.Add(go);
		go.AddComponent<Speaker>();
		go.SetActive(false);

		return s;
		
	}
	public static void PlaySound(Vector2 position, int id, bool r = false)
	{
		AudioSource s = instance.GetFreeSpeaker();
		s.gameObject.SetActive(true);
		s.transform.position = position;
		s.pitch = 1f + (r ? Random.Range(0.3f, 0.5f) : 0f);
		s.volume = r ? 0.5f : 1f;
		s.PlayOneShot(instance.clips[id]);
	}
}

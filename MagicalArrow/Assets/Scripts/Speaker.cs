using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speaker : MonoBehaviour
{
	AudioSource s;

	void Update()
    {
        if(s == null)
		{
			s = GetComponent<AudioSource>();
		}
		if (!s.isPlaying)
		{
			gameObject.SetActive(false);
		}
    }
}

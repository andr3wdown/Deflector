using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndDetector : MonoBehaviour
{
    void Update()
	{
		if(CircleShooter.allShooters.Count <= 0)
		{
			HealthController.Win();
		}
	}
}

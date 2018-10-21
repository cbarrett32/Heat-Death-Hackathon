using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyFireMars: MonoBehaviour {

	public int maxAliveTime = 1000;
	private int currentAliveTime = 0;

	// Use this for initialization
	public void Update()
	{
		currentAliveTime++;
		if (currentAliveTime > maxAliveTime) {
			Destroy (gameObject);
		}
	}

}

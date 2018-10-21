using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {
	private Transform location;
	private Rigidbody2D rb;
	public int moveSpeed = 1;

	public int maxAliveTime = 1000;               
	private int currentAliveTime = 0;            // track how long we've been alive
	private float desiredRotation;

	//current time
	private float time;

	//The time to spawn the object
	private float spawnTime;
	void Awake() {
		float startPlace = Random.value * 200;
		location = GetComponent<Transform> ();
		rb = GetComponent<Rigidbody2D> ();
		location.Translate (Vector2.right * startPlace);
		//Vector2 randomDirection = new Vector2 (Random.value*180, Random.value);
		//location.rotation = Random.rotation;
		location.Rotate(Vector3.forward, Random.Range(290, 430));

	}

	public void Attack(){
		location.Translate (Vector2.down * moveSpeed * Time.deltaTime);
		//location.Rotate(Vector)
	}


	// Use this for initialization
	void Start () {
	}

	void Update () {
		currentAliveTime++;

		if (currentAliveTime > maxAliveTime)
		{
			//Destroy( gameObject );
		}

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Attack ();
		time += Time.deltaTime;

	}
}



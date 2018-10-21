using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScriptMars : MonoBehaviour {
	private Transform location;
	private Rigidbody2D rb;
	public int moveSpeed = 1;

	void Awake()
	{
		location = GetComponent<Transform> ();
		rb = GetComponent<Rigidbody2D> ();
		float startPlace = Random.value * 45;
		location.Translate (Vector2.right * startPlace);
	}

	public void Attack(){
		location.Translate (Vector2.down * moveSpeed * Time.deltaTime);
	}


	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void FixedUpdate () {
		Attack ();
	}
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerSticky : MonoBehaviour
{
	void OnCollisionEnter2D(Collision2D col)
	{
		col.gameObject.transform.parent = gameObject.transform;

	}
	void OnCollisionExit2D(Collision2D col)
	{
		col.gameObject.transform.parent = null;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DamageableEnemy : MonoBehaviour, IDamagable
{
	public int health = 5;
	//private AudioSource asteroidDestroyAudio;
	bool hasPlayed = false;

	public void Update()
	{
//		if (hasPlayed == false) {
//			Debug.Log ("Playing audio " + hasPlayed);
//			AudioSource explosionAudio = gameObject.GetComponent<AudioSource> ();
//			Debug.Log (explosionAudio.clip.name);
//			explosionAudio.Play ();
//			//AudioSource.PlayClipAtPoint (explosionAudio.clip, transform.position);
//			hasPlayed = true;
//		}
	}

	public void TakeDamage(int damageTaken)
	{
		health -= damageTaken;

		if (health <= 0)
		{
			Debug.Log("I have been defeated!!!!");

			Debug.Log ("Playing Audio");
			AudioSource explosionAudio = gameObject.GetComponent<AudioSource> ();

			//Debug.Log (explosionAudio);

			if (explosionAudio == null) {
				Debug.Log ("Can't play due to missing audioSource");
			}

			//explosionAudio.PlayOneShot (explosionAudio.clip);
			explosionAudio.Play ();

			AudioSource.PlayClipAtPoint (explosionAudio.clip, transform.position);
			//gameObject.SetActive (false);
			gameObject.transform.localScale = Vector3.zero;

			Destroy( gameObject, 2.0f ); 
		}
	}
}

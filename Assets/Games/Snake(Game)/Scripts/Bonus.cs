
using UnityEngine;
using System.Collections;

public class Bonus : MonoBehaviour {

	public float timeout = 5;

	void Start () 
	{
		Destroy(gameObject, timeout);
	}

	void OnTriggerEnter2D(Collider2D other) 
	{
		Snake.tailCount++;
		Destroy(gameObject);
	}
}

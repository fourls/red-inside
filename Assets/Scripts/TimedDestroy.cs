using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedDestroy : MonoBehaviour {
	public float time;

	void Start() {
		Invoke("DestroyMe",time);
	}

	void DestroyMe() {
		Destroy(gameObject);
	}
}

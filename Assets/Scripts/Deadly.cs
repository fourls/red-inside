﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deadly : MonoBehaviour {
	void OnTriggerEnter2D(Collider2D other) {
		if(other.CompareTag("Player")) {
			GameManager.ins.PlayerDied();
		}
	}
}

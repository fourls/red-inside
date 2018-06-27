using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
	public static GameManager ins = null;

	[Header("Layers")]
	public LayerMask groundLayer;
	public LayerMask pickuppableLayer;
	public LayerMask shootableLayers;
	[Header("References")]
	public PlayerController player;

	// Use this for initialization
	void Awake () {
		if(ins == null) ins = this;
		else if (ins != this) Destroy(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void PlayerDied() {
		player.Respawn();
	}
}

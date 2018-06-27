using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakOnShoot : MonoBehaviour, IShootable {
	public void OnShot() {
		Destroy(gameObject);
	}
}

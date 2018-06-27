using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ConditionalObject : MonoBehaviour {
	public Sprite onSprite;
	public Sprite offSprite;
	public SpriteRenderer indicator;

	protected virtual void Update() {
		if(IsOn() && indicator.sprite == offSprite) {
			indicator.sprite = onSprite;
		} else if (!IsOn() && indicator.sprite == onSprite) {
			indicator.sprite = offSprite;
		}
	}
	
	public virtual bool IsOn() {
		return true;
	}
}

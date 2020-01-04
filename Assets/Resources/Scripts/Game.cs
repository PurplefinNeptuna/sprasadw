using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct SampahOAB3 {
	public List<Sprite> organik;
	public List<Sprite> anorganik;
	public List<Sprite> residu;
}

public class Game : MonoBehaviour {
	public static Game main;
	public SampahOAB3 sSampah;
	public float spawnY;
	public float maxSpeed = 2f;
	public SpriteRenderer sungai;

	private void Awake() {
		if(main == null) {
			main = this;
		}
		else if(main != this) {
			Destroy(gameObject);
		}

		spawnY = sungai.bounds.size.y / 2f;
	}
}

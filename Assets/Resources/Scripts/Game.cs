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
	public float defSpawnDelay = 2f;
	public float spawnDelay = 2f;
	public SpriteRenderer sungai;

	private void Awake() {
		if (main == null) {
			main = this;
		}
		else if (main != this) {
			Destroy(gameObject);
		}

		spawnY = sungai.bounds.size.y / 2f;
	}

	private void Update() {
		if (spawnDelay > 0f) {
			spawnDelay -= Time.deltaTime;
		}
		else {
			spawnDelay += defSpawnDelay;
			int tipe = UnityEngine.Random.Range(1, 4);
			Sprite gambare = null;
			if (tipe == 1) {
				gambare = sSampah.organik[UnityEngine.Random.Range(0, sSampah.organik.Count)];
			}
			else if (tipe == 2) {
				gambare = sSampah.anorganik[UnityEngine.Random.Range(0, sSampah.anorganik.Count)];
			}
			else if (tipe == 3) {
				gambare = sSampah.residu[UnityEngine.Random.Range(0, sSampah.residu.Count)];
			}
			if (gambare != null) SpawnSampah(gambare, tipe);
		}
	}

	public GameObject SpawnSampah(Sprite gambar, int tipeSampah = 0) {
		GameObject newSampah = new GameObject("sampah");
		newSampah.transform.parent = sungai.transform;
		newSampah.transform.localPosition = new Vector3(-10f, UnityEngine.Random.Range(-spawnY, spawnY), 0f);
		SpriteRenderer srender = newSampah.AddComponent<SpriteRenderer>();
		srender.sprite = gambar;
		srender.sortingLayerName = "Item";

		newSampah.AddComponent<PolygonCollider2D>();
		Rigidbody2D rb = newSampah.AddComponent<Rigidbody2D>();
		rb.gravityScale = 0f;
		Sampah objSampah = newSampah.AddComponent<Sampah>();
		objSampah.tipeSampah = tipeSampah;

		return newSampah;
	}
}

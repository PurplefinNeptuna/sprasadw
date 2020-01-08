using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

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
	public int score = 0;
	public int wrong = 0;
	public TMP_Text scoreText;
	public Image[] wrongImage;
	public GameObject gameoverPanel;
	public TMP_Text GOScore;
	public bool startSpawn = true;

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
		if(startSpawn) {
			if(spawnDelay > 0f) {
				spawnDelay -= Time.deltaTime;
			}
			else {
				spawnDelay += defSpawnDelay;
				int tipe = UnityEngine.Random.Range(1, 4);
				Sprite gambare = null;
				if(tipe == 1) {
					gambare = sSampah.organik[UnityEngine.Random.Range(0, sSampah.organik.Count)];
				}
				else if(tipe == 2) {
					gambare = sSampah.anorganik[UnityEngine.Random.Range(0, sSampah.anorganik.Count)];
				}
				else if(tipe == 3) {
					gambare = sSampah.residu[UnityEngine.Random.Range(0, sSampah.residu.Count)];
				}
				if(gambare != null)
					SpawnSampah(gambare, tipe);
			}
		}

		scoreText.text = "Score: " + score;
		if(wrong >= 1) {
			wrongImage[0].color = new Color(wrongImage[0].color.r, wrongImage[0].color.g, wrongImage[0].color.b, 1f);
		}
		if(wrong >= 2) {
			wrongImage[1].color = new Color(wrongImage[1].color.r, wrongImage[1].color.g, wrongImage[1].color.b, 1f);
		}
		if(wrong >= 3) {
			wrongImage[2].color = new Color(wrongImage[2].color.r, wrongImage[2].color.g, wrongImage[2].color.b, 1f);
			GameOver();
		}
	}

	public void GameOver() {
		startSpawn = false;
		gameoverPanel.SetActive(true);
		GOScore.text = "Score: " + score;
	}

	public void Retry() {
		SceneManager.LoadScene(0);
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

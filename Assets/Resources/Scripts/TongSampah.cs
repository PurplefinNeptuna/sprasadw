using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TongSampah : MonoBehaviour
{
	public int tipe;
	private void OnTriggerStay2D(Collider2D collision) {
		Sampah sampah = collision.GetComponent<Sampah>();
		Bounds bounds = GetComponent<Collider2D>().bounds;
		if(!sampah.drag && bounds.Contains(collision.transform.position)) {
			if(sampah.tipeSampah == tipe) {
				Game.main.score++;
			}
			else {
				Game.main.wrong++;
			}
			Destroy(collision.gameObject);
		}
	}
}

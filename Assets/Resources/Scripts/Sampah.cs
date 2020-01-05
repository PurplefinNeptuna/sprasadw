using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sampah : MonoBehaviour {
	public int tipeSampah;
	private void Update() {
		Rigidbody2D body = GetComponent<Rigidbody2D>();
		body.AddForce(Vector2.right);
		body.velocity = Mathf.Min(body.velocity.magnitude, Game.main.maxSpeed) * body.velocity.normalized;
	}
}

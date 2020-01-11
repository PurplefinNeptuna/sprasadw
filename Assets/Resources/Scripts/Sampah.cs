using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sampah : MonoBehaviour {
	public int tipeSampah;
	public bool drag = false;
	public Rigidbody2D body;
	public float sampahTimer = 3f;

	private void Start() {
		body = GetComponent<Rigidbody2D>();
		body.AddTorque(Random.Range(-3f, 3f));
		body.drag = 3f;
	}

	private void Update() {
		if (!drag) {
			body.velocity = Mathf.Min(body.velocity.magnitude, Game.main.maxSpeed) * body.velocity.normalized;
		}
		else {
			body.velocity = Vector2.zero;
			body.angularVelocity = 0f;
		}
		if (transform.localPosition.x > 10f) {
			Game.main.wrong++;
			Destroy(gameObject);
		}

		if (Game.main.wrong >= 3) {
			Destroy(gameObject);
		}

		sampahTimer -= Time.deltaTime;
		if (sampahTimer <= 0f) {
			Game.main.wrong++;
			Destroy(gameObject);
		}
	}
}

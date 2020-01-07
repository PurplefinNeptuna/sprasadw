using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sampah : MonoBehaviour {
	public int tipeSampah;
	public bool drag = false;
	public Rigidbody2D body;

	private void Start() {
		body = GetComponent<Rigidbody2D>();
		body.AddTorque(Random.Range(-3f,3f));
	}

	private void Update() {
		if (!drag) {
			body.AddForce(Vector2.right);
			body.velocity = Mathf.Min(body.velocity.magnitude, Game.main.maxSpeed) * body.velocity.normalized;
		}
		else {
			body.velocity = Vector2.zero;
		}
		if (transform.localPosition.x > 10f) Destroy(gameObject);
	}

	private void OnMouseDown() {
		drag = true;
	}

	private void OnMouseDrag() {
		Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, -1 * (Camera.main.transform.position.z));
		Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
		transform.position = objPosition;
	}

	private void OnMouseUp() {
		drag = false;
	}
}

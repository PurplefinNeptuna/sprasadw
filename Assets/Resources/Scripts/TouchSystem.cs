﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IDCircle {
	public int id;
	public GameObject circle;

	public IDCircle(int id, GameObject circle) {
		this.id = id;
		this.circle = circle;
	}
}

public class TouchSystem : MonoBehaviour {
	private Touch theTouch;
	private Touch[] touches;
	private List<IDCircle> pairs;
	private string multiTouchInfo;
	private GameObject DebugCircle;

	private void Awake() {
		DebugCircle = Resources.Load<GameObject>("Prefabs/Circle");
		pairs = new List<IDCircle>();
	}

	private void Update() {
		touches = Input.touches;
		TouchDebug();
		for (int i = 0; i < touches.Length; i++) {
			theTouch = touches[i];
			if (theTouch.phase == TouchPhase.Began) {
				pairs.Add(new IDCircle(theTouch.fingerId, SpawnCircle(theTouch)));
			}
			else if (theTouch.phase == TouchPhase.Ended) {
				IDCircle nowtouch = pairs.Find(IDCircle => IDCircle.id == theTouch.fingerId);
				if (nowtouch != null) {
					Destroy(nowtouch.circle);
					pairs.RemoveAt(pairs.IndexOf(nowtouch));
				}
			}
			else if (theTouch.phase == TouchPhase.Moved) {
				IDCircle nowtouch = pairs.Find(IDCircle => IDCircle.id == theTouch.fingerId);
				if (nowtouch != null) {
					float scale = Mathf.Max(GetTouchSize(theTouch.radius), .3f) * 2f;
					nowtouch.circle.transform.position = GetTouchPosition(theTouch.position);
					nowtouch.circle.transform.localScale = new Vector3(scale, scale, 1f);
				}
			}
		}
		Touch mouseTouch = new Touch();
		mouseTouch.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
		mouseTouch.radius = 0.3f;
		mouseTouch.tapCount = 1;
		mouseTouch.fingerId = 99;

		if (Input.GetMouseButtonDown(0)) {
			pairs.Add(new IDCircle(mouseTouch.fingerId, SpawnCircle(mouseTouch)));
		}
		else if (Input.GetMouseButtonUp(0)) {
			IDCircle nowtouch = pairs.Find(IDCircle => IDCircle.id == mouseTouch.fingerId);
			if (nowtouch != null) {
				Destroy(nowtouch.circle);
				pairs.RemoveAt(pairs.IndexOf(nowtouch));
			}
		}
		else if (Input.GetMouseButton(0)) {
			IDCircle nowtouch = pairs.Find(IDCircle => IDCircle.id == mouseTouch.fingerId);
			if (nowtouch != null) {
				nowtouch.circle.transform.position = GetTouchPosition(mouseTouch.position);
			}
		}
	}

	private void TouchDebug() {
		multiTouchInfo = "";
		for (int i = 0; i < touches.Length; i++) {
			theTouch = touches[i];
			multiTouchInfo += string.Format("Touch {0}:\n\tPosition {1}\n\tRadius:{2} ({3} Units)\n\n",
				i, theTouch.position, theTouch.radius,
				GetTouchSize(theTouch.radius));
		}
		if (Input.GetMouseButton(0)) {
			multiTouchInfo += string.Format("Mouse:\n\tPosition {0}\n\n",
				new Vector2(Input.mousePosition.x, Input.mousePosition.y));
		}
		Game.main.DebugLog.text = multiTouchInfo;
	}

	private Vector2 GetTouchPosition(Vector2 touchPosition) {
		return Camera.main.ScreenToWorldPoint(new Vector3(touchPosition.x, touchPosition.y, transform.position.z));
	}

	private float GetTouchSize(float radius) {
		return Camera.main.ScreenToWorldPoint(new Vector3(radius + (Camera.main.pixelWidth / 2f), (Camera.main.pixelHeight / 2f), 0f)).x;
	}

	private GameObject SpawnCircle(Touch t) {
		float scale = Mathf.Max(GetTouchSize(t.radius), .3f) * 2f;
		GameObject c = Instantiate(DebugCircle);
		TouchArea ta = c.GetComponent<TouchArea>();
		c.name = "Touch" + t.fingerId;
		c.transform.position = GetTouchPosition(t.position);
		c.transform.localScale = new Vector3(scale, scale, 1f);
		ta.radius = Mathf.Max(GetTouchSize(t.radius), .3f);
		return c;
	}
}

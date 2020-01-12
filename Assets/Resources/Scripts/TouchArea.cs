using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TouchArea : MonoBehaviour {
	public float radius;
	public GameObject sampah = null;

	private void Start() {
		List<Collider2D> kena = Physics2D.OverlapCircleAll(transform.position, radius, LayerMask.GetMask(new string[] {"Sampah"})).ToList();
		if(kena.Count > 0) {
			kena.Sort((x1, x2) => Mathf.Abs((x1.transform.position - transform.position).magnitude).CompareTo(Mathf.Abs((x2.transform.position - transform.position).magnitude)));
			sampah = kena[0].gameObject;
			Sampah sampahScript = sampah?.GetComponent<Sampah>();
			if(sampahScript != null) {
				sampahScript.drag = true;
			}
		}
	}

	private void Update() {
		if(sampah != null) {
			sampah.transform.position = transform.position;
		}
	}

	private void OnDestroy() {
		if(sampah != null) {
			Sampah sampahScript = sampah?.GetComponent<Sampah>();
			if(sampahScript != null) {
				sampahScript.drag = false;
			}
		}
	}
}

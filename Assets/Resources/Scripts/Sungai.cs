using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sungai : MonoBehaviour {
	private void OnTriggerStay2D(Collider2D other) {
		Sampah sampah = other.GetComponent<Sampah>();
		if(sampah!=null){
			sampah.sampahTimer = 3f;
		}
	}
}

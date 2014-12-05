using UnityEngine;
using System.Collections;

public class Bird : MonoBehaviour {



	void OnTriggerEnter2D(Collider2D coll) {
		
		if (coll.gameObject.tag == "bullet") {
			Destroy(this.gameObject);
			Destroy(coll.gameObject);
			
		}
		
	}
}

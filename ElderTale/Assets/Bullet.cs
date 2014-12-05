using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public float aliveTime;

	// Use this for initialization
	void Start () {
		aliveTime = 0;
	
	}
	
	// Update is called once per frame
	void Update () {
		aliveTime = aliveTime + Time.deltaTime;

		if (aliveTime > 5) {
			Destroy(this.gameObject);
		}
	
	}

	/*
	void OnCollisionEnter2D(Collision2D coll) {

		if (coll.gameObject.name == "DogBoss_0") {
			Destroy(this.gameObject);
			
		}
		
	}*/

	void OnTriggerEnter2D(Collider2D coll) {
		
		if (coll.gameObject.name == "Door" && coll.gameObject.GetComponent<SpriteRenderer>().color.a == 1) {
			Debug.Log("Door");
			Destroy(this.gameObject);
			
		}

		if(coll.gameObject.name == "stoneCenter_1" || coll.gameObject.name == "stoneCenter_rounded"){
			Destroy(this.gameObject);

		}
		
	}




}

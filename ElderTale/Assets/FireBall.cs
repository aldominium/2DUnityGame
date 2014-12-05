using UnityEngine;
using System.Collections;

public class FireBall : MonoBehaviour {

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
		
		if (coll.gameObject.tag == "MyPlayer2") {
			Debug.Log("Hit Player");
			coll.GetComponent<Player>().DamagePlayer(100);
			Destroy(this.gameObject);
			
		}
		
	}
	

}

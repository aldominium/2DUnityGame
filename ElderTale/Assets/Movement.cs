using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {
	public float moveSpeed;
	public float h;
	public bool rightDirection = true;

	// Use this for initialization
	void Start () {
		h = Input.GetAxis ("Horizontal")*moveSpeed;
		//move ();
	}
	
	// Update is called once per frame
	void Update () {

		if (rightDirection) {

			transform.Translate (Vector2.right * Time.deltaTime*moveSpeed);

		} else {
			transform.Translate ( (Vector2.right*-1) * Time.deltaTime*moveSpeed);

		}

	}

	void move(){
		InvokeRepeating ("moveRandom",1,2.0f);

	}

	/*
	void moveRandom(){
		Debug.Log ("Move Random");

		if (rightDirection) {

			rightDirection = false;

		} else {

			rightDirection = true;

		}



	}
	*/

	void OnCollisionEnter2D(Collision2D coll) {

		if (coll.gameObject.name == "stoneCenter_1" || coll.gameObject.name == "stoneCenter_2" || coll.gameObject.name == "slice01_01"
		    || coll.gameObject.name == "stoneCenter_rounded1") {


			if (rightDirection) {
				
				rightDirection = false;
				
			} else {
				
				rightDirection = true;
				
			}

		}
		
		
	}

	void OnTriggerEnter2D(Collider2D coll) {
		
		if (coll.gameObject.tag == "bullet") {
			Destroy(this.gameObject);
			Destroy(coll.gameObject);
			
		}
		
	}


}










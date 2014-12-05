using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {
	BoxCollider2D boxCollider;
	public Transform player;
	SpriteRenderer renderer;

	// Use this for initialization
	void Start () {
		boxCollider = GetComponent<BoxCollider2D> ();
		renderer = GetComponent<SpriteRenderer> ();
		player = GameObject.FindGameObjectWithTag ("MyPlayer2").GetComponent<Transform> ();

		this.renderer.color = new Color (this.renderer.color.r,this.renderer.color.g,this.renderer.color.b,0);
	}
	
	// Update is called once per frame
	void Update () {
		if(player != null)
		if(player.position.x > 84f) {
			boxCollider.isTrigger = false;
			this.renderer.color = new Color (this.renderer.color.r,this.renderer.color.g,this.renderer.color.b,1);
		}
	
	}
}

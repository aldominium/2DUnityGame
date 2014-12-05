using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public bool RighDirection = true;
	public Transform BulletPrefab;

	[System.Serializable]
	public class PlayerStats{
	
		public int Health = 1000;


	
	}

	void Start(){

	}

	void Update(){

		RighDirection = this.GetComponent<PlatformerCharacter2D> ().facingRight;

		if (Input.GetMouseButtonUp (0)) {

				Transform clone;

			clone = (Transform)Instantiate (BulletPrefab, transform.FindChild("SpawnPoint").position, this.GetComponent<Transform> ().rotation);

				if (RighDirection) {

						clone.GetComponent<Rigidbody2D> ().velocity = new Vector2 (20, 0);

				} else {

						clone.GetComponent<Rigidbody2D> ().velocity = new Vector2 (-20, 0);

				}
			//childObject.animation.Play("PlayerAttackAnim");

				

						
		}
		
			
	}

	public PlayerStats playerStats = new PlayerStats();

	public void DamagePlayer(int damage){
		playerStats.Health -= damage;

		if(playerStats.Health <= 0){
			Debug.Log("Kill Player");
			GameMaster.KillPlayer(this);
		}
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.name == "liquidLavaTop") {
			DamagePlayer(1000);
			
		}else if(coll.gameObject.name == "BearWalk1"){
			DamagePlayer(1000);
		}else if (coll.gameObject.name == "DogBoss_0"){
			DamagePlayer(100);
		}else if (coll.gameObject.name == "EvilCat_0"){
			DamagePlayer(1000);
		}else if (coll.gameObject.name == "EvilChicken_0"){
			DamagePlayer(1000);
		}
		
		
	}

	void OnGUI(){
		GUILayout.Label (playerStats.Health.ToString());
	}


}

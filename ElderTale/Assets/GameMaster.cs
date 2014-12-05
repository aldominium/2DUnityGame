using UnityEngine;
using System.Collections;

public class GameMaster : MonoBehaviour {

	public static GameMaster gm;
	public bool isPlayerMoving;
	public Transform player;
	public float playerPos;

	void Start(){
		if (gm == null) {
			gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
		}

		player = GameObject.FindGameObjectWithTag ("MyPlayer2").GetComponent<Transform> ();
		playerPos =   player.position.x;
	}

	public Transform playerPrefab;
	public Transform reSpawnPoint;
	public int spawnDelay = 2;






	void Update(){

		Debug.Log (reSpawnPoint.position.x);
	}


	public IEnumerator RespawnPlayer(){
		Debug.Log ("Wsiting for spawn sound");
		yield return new WaitForSeconds (spawnDelay);
		Instantiate (playerPrefab,reSpawnPoint.position,reSpawnPoint.rotation);
		Debug.Log ("Respawn player");
	}

	public static void KillPlayer(Player player){
		Destroy (player.gameObject);
		//gm.StartCoroutine (gm.RespawnPlayer());
		Application.LoadLevel (Application.loadedLevel);
	}

	public static void KillEnemy(AI enemy){
		Destroy (enemy.gameObject);
	}

	public static void PlayerMoving(bool isMoving){
		gm.isPlayerMoving = isMoving;
	}


}

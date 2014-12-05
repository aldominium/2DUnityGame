using UnityEngine;
using System.Collections;

public class Position : MonoBehaviour {

	public Transform[] childs;
	public float distanceToPlayer;

	public Transform parent;


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

		if (GameObject.FindGameObjectWithTag ("MyPlayer2") != null) {

			distanceToPlayer = Vector2.Distance (this.GetComponent<Transform> ().position, GameObject.FindGameObjectWithTag ("MyPlayer2").GetComponent<Transform> ().position);
			//Debug.Log(GameObject.FindGameObjectWithTag ("MyPlayer2").GetComponent<Transform> ().position.x);
		} else {
			Debug.Log("No player?");
		}
	

	}
}

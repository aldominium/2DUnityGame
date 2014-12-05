using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AI : MonoBehaviour {

	public Transform currentPos;
	public Transform[] posArray;

	//Variables para guardar la distancia de cada uno de las posiciones
	//al jugador
	public float pos1Distance;
	public float pos2Distance;
	public float pos3Distance;
	public float pos4Distance;
	public float pos5Distance;
	public float pos6Distance;

	public Transform BulletPrefab;

	private LinkedList<Transform> open;
	private LinkedList<Transform> explored;

	public Vector3 playerPos;

	public float bulletSpeed = 20;


	public float Health = 1000;

	// Use this for initialization
	void Start () {
		this.GetComponent<Transform> ().position = currentPos.position;
		InvokeRepeating("findEnemy", 2, 1F);
		playerPos =  GameObject.FindGameObjectWithTag ("MyPlayer2").GetComponent<Transform> ().position;
	}
	
	// Update is called once per frame
	void Update () {
		pos1Distance = posArray [0].GetComponent<Position> ().distanceToPlayer;
		pos2Distance = posArray [1].GetComponent<Position> ().distanceToPlayer;
		pos3Distance = posArray [2].GetComponent<Position> ().distanceToPlayer;
		pos4Distance = posArray [3].GetComponent<Position> ().distanceToPlayer;
		pos5Distance = posArray [4].GetComponent<Position> ().distanceToPlayer;
		pos6Distance = posArray [5].GetComponent<Position> ().distanceToPlayer;
		if(GameObject.FindGameObjectWithTag ("MyPlayer2") != null)
			playerPos =  GameObject.FindGameObjectWithTag ("MyPlayer2").GetComponent<Transform> ().position;
	
	}

	//TODO - buscar
	void findEnemy(){

		currentPos.parent = null;
		open = new LinkedList<Transform> ();
		open.AddFirst (currentPos);
		explored = new LinkedList<Transform> ();

		while(open.Count > 0){

			//Debug.Log("count > 0");

			Transform bestNode = getBestNode(open);
			//Debug.Log( "Best Node: " + bestNode.GetComponent<Position>().name );
			open.Remove(bestNode);
			explored.AddLast(bestNode);

			if(isGoal(bestNode)){
				Transform clone;

				currentPos = bestNode;
				LinkedList<Position> path = constructPath( bestNode.GetComponent<Position>() );
				this.GetComponent<Transform> ().position = path.First.Value.GetComponent<Transform>().position;

				clone = (Transform)Instantiate (BulletPrefab, transform.position, transform.rotation);
				bool pointingRight = true;

				if(transform.position.x < playerPos.x){
					
					clone.GetComponent<Rigidbody2D> ().velocity = new Vector2 (bulletSpeed, 0);
				}else{
					clone.GetComponent<Rigidbody2D> ().velocity = new Vector2 (bulletSpeed * -1, 0);
					pointingRight = false;

				}



				if(pointingRight){
					if(transform.localScale.x > 0)
						transform.localScale = new Vector3(transform.localScale.x*-1,transform.localScale.y,transform.localScale.z);
				
				}else{
					if(transform.localScale.x < 0)
						transform.localScale = new Vector3(transform.localScale.x*-1,transform.localScale.y,transform.localScale.z);


				}

				return;

			}


			for(int i = 0; i < bestNode.GetComponent<Position>().childs.Length; i++){
				//Debug.Log("numero Hijos"+bestNode.GetComponent<Position>().childs.Length);

				if(! contiene( bestNode.GetComponent<Position>().childs[i].GetComponent<Position>() ) ){

					bestNode.GetComponent<Position>().childs[i].parent = bestNode;
					open.AddLast(bestNode.GetComponent<Position>().childs[i]);
				//	Debug.Log("añadi"+bestNode.GetComponent<Position>().childs[i].name);

				}//endif

			}//endfor




		}

		//Transform bestNode = getBestNode (open);

		//Debug.Log (bestNode.GetComponent<Position>().name);

	}



	bool isGoal(Transform pos){
		if(pos.GetComponent<Position> ().distanceToPlayer <= 3){
			return true;
		}else{
			return false;
		}

	}

	Transform getBestNode(LinkedList<Transform> list){

		//LinkedListNode<Transform> bestNode;
		//bestNode = list.First;

		Transform bestNode = list.First.Value;
		//string name = bestNode.GetComponent<GameObject>().name;

		foreach(Transform n in list){
			if( n.GetComponent<Position>().distanceToPlayer <= bestNode.GetComponent<Position>().distanceToPlayer){
				bestNode = n;

			}
		}

		return bestNode;

	}

	bool contiene(Position pos){
		
		//LinkedListNode<Transform> bestNode;
		//bestNode = list.First;
		
		//Transform bestNode = list.First.Value;
		//string name = bestNode.GetComponent<GameObject>().name;
		
		foreach(Transform n in explored){
			if( n.GetComponent<Position>().name.Equals(pos.name)){
				return true;
				
			}
		}
		
		return false;
		
	}

	LinkedList<Position> constructPath(Position pos){
		Debug.Log ("Construct Path");
		//Debug.Log (pos.GetComponent<Transform>().parent.name);
		LinkedList<Position> path = new LinkedList<Position> ();

		while (pos.GetComponent<Transform>().parent != null) {
			Debug.Log(pos.name);
			path.AddFirst(pos);
			pos = pos.GetComponent<Transform>().parent.GetComponent<Position>();

		}
		path.AddFirst (pos);
		Debug.Log(pos.name);

		return path;
	
	}
	void OnTriggerEnter2D(Collider2D coll) {
		
		if (coll.gameObject.tag == "bullet") {
			Debug.Log("Trigger monster");
			DamageEnemy(100);
			Destroy(coll.gameObject);
			
		}
		
	}


	public void DamageEnemy(int damage){
		Health -= damage;
		
		if(Health <= 0){
			Debug.Log("Kill Enemy");
			GameMaster.KillEnemy(this);
		}
	}



}









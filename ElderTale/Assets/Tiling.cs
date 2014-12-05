using UnityEngine;
using System.Collections;


[RequireComponent (typeof(SpriteRenderer))]

public class Tiling : MonoBehaviour {

	public int offsetX = 2;			// Offset al momento de crear el nuevo objeto
	
	// Estas se usan para saber si necesitamos crear escenario en izquierda o derecha
	public bool hasARightBuddy = false;
	public bool hasALeftBuddy = false;
	
	public bool reverseScale = false;	// Cuando no estan conectados los bordes izquierda y derecha

	//variables de testeo(no tocar)
	public float camOrtoSize;
	public float camXExtend;
	public float camTransX;
	
	private float spriteWidth = 0f;		// the width of our element
	private Camera cam;					// Referencia a la camara principal
	private Transform myTransform;      //Esto hace referencia a las propiedades de transformacion de nuestro objeto



	//Metodo que se llama justo antes de start. Ideal para agarrar referencias a objetos
	void Awake () {
		cam = Camera.main;
		myTransform = transform;   //aqui transform hace referencia al objeto al que le añadimos el script
	}
	
	// Use this for initialization
	void Start () {
		SpriteRenderer sRenderer = GetComponent<SpriteRenderer>(); //necesitamos la instancia del sprite renderer para hacer crear nuevos sprites
		spriteWidth = sRenderer.sprite.bounds.size.x; //el ancho del sprite es igual al tamaño del sprite en x
		camOrtoSize = cam.orthographicSize;

	}


	// Update is called once per frame
	void Update () {
		camTransX = cam.transform.position.x;
		camXExtend = cam.orthographicSize * Screen.width/Screen.height;


		// does it still need buddies? If not do nothing
		if (hasALeftBuddy == false || hasARightBuddy == false) {

			// calcula la extension de la camara (mitad del tamaño) de lo que la camara puede ver en coordenadas del mundo
			float camHorizontalExtend = cam.orthographicSize * Screen.width/Screen.height;
			
			// calcula la posicion en x donde la camara puede ver el borde del sprit
			float edgeVisiblePositionRight = (myTransform.position.x + spriteWidth/2) - camHorizontalExtend;
			float edgeVisiblePositionLeft = (myTransform.position.x - spriteWidth/2) + camHorizontalExtend;
			
			// checking if we can see the edge of the element and then calling MakeNewBuddy if we can
			if (cam.transform.position.x >= edgeVisiblePositionRight - offsetX && hasARightBuddy == false)
			{
				MakeNewBuddy (1);
				hasARightBuddy = true;
			}
			else if (cam.transform.position.x <= edgeVisiblePositionLeft + offsetX && hasALeftBuddy == false)
			{
				MakeNewBuddy (-1);
				hasALeftBuddy = true;

			}



		}//endIf

	}//endUpdate



	// funcion que crea un sprite en el lado requerido
	void MakeNewBuddy (int rightOrLeft) {

		// Calcula la posicion del nuevo sprite
		Vector3 newPosition = new Vector3 (myTransform.position.x + spriteWidth * rightOrLeft, myTransform.position.y, myTransform.position.z);

		// instancia el nuevo sprite y lo guarda en una variable
		Transform newBuddy = Instantiate (myTransform, newPosition, myTransform.rotation) as Transform;
		
		// Si no esta hecho como un tile, lo invertimos en x para que quede bien
		if (reverseScale == true) {
			newBuddy.localScale = new Vector3 (newBuddy.localScale.x*-1, newBuddy.localScale.y, newBuddy.localScale.z);
		}
		//El padre de este tile es el mismo que el del tile pasado
		newBuddy.parent = myTransform.parent;

		//Si es mayor que 0 quiere decir que el anterior tile estaba en el lado izquierdo
		if (rightOrLeft > 0) {
			newBuddy.GetComponent<Tiling>().hasALeftBuddy = true;
		}
		else {
			newBuddy.GetComponent<Tiling>().hasARightBuddy = true;
		}
	}

}






















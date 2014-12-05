using UnityEngine;
using System.Collections;

public class Parallaxing : MonoBehaviour {

	public Transform[] backgrounds;          //Todos los backgrounds del efecto parallax
	private float[] paralaxScales;
	public float smothing = 1f;


	private Transform cam;				     //reference to main cameras transform
	private Vector3 previousCamPosition;      //The position of the camera un the previous frame

	//Es llamado antes de start(). Bueno para hacer referencias necesarias a los objetos
	void Awake(){
		//Ponemos la referencia a la camara
		cam = Camera.main.transform;

	}
	
	// Use this for initialization
	void Start () {
		//El anterior frame tenia la posicion de la camara en el frame actual
		previousCamPosition = cam.position;

		//Se asigna la escala parallax correspondiente
		paralaxScales = new float[backgrounds.Length];
		for(int i = 0; i < backgrounds.Length; i++){
			paralaxScales[i] = backgrounds[i].position.z*-1;
		}


	}
	
	// Update is called once per frame
	void Update () {

		for(int i = 0; i< backgrounds.Length; i++){
			float parallax = (previousCamPosition.x - cam.position.x) * paralaxScales[i];

			float backgroundTargetPosX = backgrounds[i].position.x + parallax;

			Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX,backgrounds[i].position.y,backgrounds[i].position.z);

			backgrounds[i].position = Vector3.Lerp(backgrounds[i].position,backgroundTargetPos,smothing*Time.deltaTime);

		}

		//cambiar la previousCamPos a la posicion de la camara al final del frame
		previousCamPosition = cam.position;

	}
}










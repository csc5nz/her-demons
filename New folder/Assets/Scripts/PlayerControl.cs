using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

	public float speed = 10.0F;
	public string facing;
	private bool movingX = false;
	private bool movingZ = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		float translationX = Input.GetAxis ("Vertical") * speed;
		float translationZ = Input.GetAxis ("Horizontal") * speed;

		if (translationX != 0 && translationZ == 0){
			movingZ = false;
			movingX = true;
		}

		if(translationZ != 0 && translationX == 0) {
			movingX = false;
			movingZ = true;
		}

		if (translationX == 0 && translationZ == 0) {
			movingX = false;
			movingZ = false;
		}

		if (movingX == true) {
			translationZ = 0;
		}

		if (movingZ == true) {
			translationX = 0;
		}

		if (translationZ == 0) {
			translationX *= Time.deltaTime;
			transform.Translate (translationX, 0, 0);
		}

		if (translationX == 0) {
			translationZ *= Time.deltaTime;
			transform.Translate (0, 0, -translationZ);
		}


	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

	public float speed = 2.5F;

	private Vector3 pos;
	private Transform tr;
	private int imagePosition; 
	private int newimagePosition;

	// Use this for initialization
	void Start () {
		pos = transform.position;
		tr = transform;
		imagePosition = 0;
		newimagePosition = 0;
	}

	// Update is called once per frame
	void Update () {

		if (Input.GetKey (KeyCode.D) && tr.position == pos) {
			pos += 2*Vector3.back;
			newimagePosition = 3;
		}
		if (Input.GetKey (KeyCode.A) && tr.position == pos) {
			pos += 2*Vector3.forward;
			newimagePosition = 1;
		}
		if (Input.GetKey (KeyCode.W) && tr.position == pos) {
			pos += 2*Vector3.right;
			newimagePosition = 0;
		}
		if (Input.GetKey (KeyCode.S) && tr.position == pos) {
			pos += 2*Vector3.left;
			newimagePosition = 2;
		}

		transform.position = Vector3.MoveTowards (transform.position, pos, Time.deltaTime * speed);
		transform.Rotate (0, 90 * (imagePosition - newimagePosition), 0);

		imagePosition = newimagePosition;
	}

	void rotateRight(){
	}
	void rotateLeft(){
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

	public float speed = 2.5F;

	private Vector3 pos;
	private Transform tr;
	private int faceDirection; 
	private int newfaceDirection;
	private bool can = true;

	// Use this for initialization
	void Start () {
		pos = transform.position;
		tr = transform;
		faceDirection = 0;
		newfaceDirection = 0;
	}

	// Update is called once per frame
	void Update () {
		move (can);

	}

	public void elevatorup(){
		Vector3 dest = transform.position + 4 * Vector3.up;
		while(transform.position.y < dest.y ){
			transform.position = Vector3.MoveTowards (transform.position, dest, Time.deltaTime * speed);
		}

	}

	public void elevatordown(){
	}

	public void move(bool can){
		if (Input.GetKey (KeyCode.D) && tr.position == pos) {
			pos += 2*Vector3.back;
			newfaceDirection = 3;
		}
		if (Input.GetKey (KeyCode.A) && tr.position == pos) {
			pos += 2*Vector3.forward;
			newfaceDirection = 1;
		}
		if (Input.GetKey (KeyCode.W) && tr.position == pos) {
			pos += 2*Vector3.right;
			newfaceDirection = 0;
		}
		if (Input.GetKey (KeyCode.S) && tr.position == pos) {
			pos += 2*Vector3.left;
			newfaceDirection = 2;
		}

		transform.position = Vector3.MoveTowards (transform.position, pos, Time.deltaTime * speed);
		transform.Rotate (0, 90 * (faceDirection - newfaceDirection), 0);

		faceDirection = newfaceDirection;
	}
}

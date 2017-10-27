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

	public LayerMask blockingLayer;	
	public float moveTime = 0.1f;	
	private Rigidbody rigidbody;

	// Use this for initialization
	void Start () {
		pos = transform.position;
		tr = transform;
		faceDirection = 0;
		newfaceDirection = 0;
	}

	// Update is called once per frame
	void Update () {
		move ();


	}

	public void move ()
	{

		Vector3 curr = tr.position;
		Vector3 currback = curr + 2 * Vector3.back;
		Vector3 currforward = curr + 2 * Vector3.forward;
		Vector3 currright = curr + 2 * Vector3.right;
		Vector3 currleft = curr + 2 * Vector3.left;

		bool rightBlocked = Physics.Linecast (curr, currback);
		bool leftBlocked = Physics.Linecast (curr, currforward);
		bool forwardBlocked = Physics.Linecast (curr, currright);
		bool backBlocked = Physics.Linecast (curr, currleft);
		print ("right" + rightBlocked);
		print ("left" + leftBlocked);
		print ("forward" + forwardBlocked);
		print ("back" + backBlocked);

		if (Input.GetKey (KeyCode.D) && tr.position == pos) {
			if (!rightBlocked) {
				pos += 2 * Vector3.back;
			}
			newfaceDirection = 3;
		}
		if (Input.GetKey (KeyCode.A) && tr.position == pos) {
			if (!leftBlocked) {
				pos += 2 * Vector3.forward;
			}
			newfaceDirection = 1;
		}
		if (Input.GetKey (KeyCode.W) && tr.position == pos) {
			if (!forwardBlocked) {
				pos += 2 * Vector3.right;
			}
			newfaceDirection = 0;
		}
		if (Input.GetKey (KeyCode.S) && tr.position == pos) {
			if (!backBlocked) {
				pos += 2 * Vector3.left;
			}
			newfaceDirection = 2;
		}

		if (Input.GetKey (KeyCode.P) && tr.position == pos) {
			pos += 4 * Vector3.up;
		}

		if (Input.GetKey (KeyCode.O) && tr.position == pos) {
			pos += 4 * Vector3.down;
		}

		transform.position = Vector3.MoveTowards (transform.position, pos, Time.deltaTime * speed);
		transform.Rotate (0, 90 * (faceDirection - newfaceDirection), 0);

		faceDirection = newfaceDirection;
	}

}

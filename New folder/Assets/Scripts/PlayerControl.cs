using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

	public float walkspeed = 2.5F;
	public float runspeed = 5F;
	public Animator animator;

	private Vector3 pos;
	private Vector3 pos2;
	private Transform tr;
	private int faceDirection; 
	private int newfaceDirection;
	private bool onstair;

	public LayerMask blockingLayer;	
	private Rigidbody rigidbody;
	private bool walk = true;

	// Use this for initialization
	void Start () {
		onstair = false;
		pos = transform.position;
		pos2 = pos;
		tr = transform;
		faceDirection = 0;
		newfaceDirection = 0;
	}

	// Update is called once per frame
	void Update () {
		move ();
	}

	void attack ()
	{
		Vector3 curr = tr.position;
		if (faceDirection == 0) {
			
		}	
	}

	public void move ()
	{
		RaycastHit hitObjectBack;
		RaycastHit hitObjectForward;
		RaycastHit hitObjectRight;
		RaycastHit hitObjectLeft;

		Vector3 curr = tr.position;
		Vector3 currback = curr + 2 * Vector3.back;
		Vector3 currforward = curr + 2 * Vector3.forward;
		Vector3 currright = curr + 2 * Vector3.right;
		Vector3 currleft = curr + 2 * Vector3.left;

		bool rightBlocked = Physics.Linecast (curr, currback, out hitObjectRight); // is there a collider in the direction that the character is attempting to move to? 
		bool leftBlocked = Physics.Linecast (curr, currforward, out hitObjectLeft);
		bool forwardBlocked = Physics.Linecast (curr, currright, out hitObjectForward);
		bool backBlocked = Physics.Linecast (curr, currleft, out hitObjectBack);

		if (Input.GetKey (KeyCode.D) && tr.position == pos2) { // moving right
			if (rightBlocked && hitObjectRight.collider.tag == "stair") {
				if ((tr.transform.position.y - 1) % 4 == 0) {
					pos += 1 * Vector3.back; 
					pos2 = pos + 1 * Vector3.up + 1 * Vector3.back;
				}
				else if ((tr.transform.position.y - 1) % 4 == 1) {
					pos += 2* Vector3.up + 2 * Vector3.back; 
					pos2 = pos;
				}
				else if ((tr.transform.position.y - 1) % 4 == 3) {
					pos += 1* Vector3.up + 1 * Vector3.back; 
					pos2 = pos + 1 * Vector3.back;
				}
			}
			if (rightBlocked && hitObjectRight.collider.tag == "stairdown") {
				if ((tr.transform.position.y - 1) % 4 == 1) {
					pos += 1* Vector3.down + 1 * Vector3.back; 
					pos2 = pos + 1 * Vector3.back;
				}
				else if ((tr.transform.position.y - 1) % 4 == 3) {
					pos += 2* Vector3.down + 2 * Vector3.back; 
					pos2 = pos;
				}
				else if ((tr.transform.position.y - 1) % 4 == 0) {
					pos += 1 * Vector3.back; 
					pos2 = pos + 1* Vector3.down + 1 * Vector3.back;
				}
			}
			if (!rightBlocked) {
				pos += 2 * Vector3.back;
				pos2 = pos;
			}
			newfaceDirection = 3;
		}
		if (Input.GetKey (KeyCode.A) && tr.position == pos2) { // moving left
			if (leftBlocked && hitObjectLeft.collider.tag == "stair") {
				if ((tr.transform.position.y - 1) % 4 == 1) {
					pos += 1 * Vector3.forward; 
					pos2 = pos + 1 * Vector3.up + 1 * Vector3.forward;
				}
				else if ((tr.transform.position.y - 1) % 4 == 1) {
					pos += 2* Vector3.up + 2 * Vector3.forward; 
					pos2 = pos;
				}
				else if ((tr.transform.position.y - 1) % 4 == 3) {
					pos += 1* Vector3.up + 1 * Vector3.forward; 
					pos2 = pos + 1 * Vector3.forward;
				}
			}
			if (leftBlocked && hitObjectLeft.collider.tag == "stairdown") {
				if ((tr.transform.position.y - 1) % 4 == 1) {
					pos += 1* Vector3.down + 1 * Vector3.forward; 
					pos2 = pos + 1 * Vector3.forward;
				}
				else if ((tr.transform.position.y - 1) % 4 == 3) {
					pos += 2* Vector3.down + 2 * Vector3.forward; 
					pos2 = pos;
				}
				else if ((tr.transform.position.y - 1) % 4 == 0) {
					pos += 1 * Vector3.forward; 
					pos2 = pos + 1* Vector3.down + 1 * Vector3.forward;
				}
			}
			if (!leftBlocked) {
				pos += 2 * Vector3.forward;
				pos2 = pos;
			}
			newfaceDirection = 1;
		}
		if (Input.GetKey (KeyCode.W) && tr.position == pos2) { // moving forward
			if (forwardBlocked && hitObjectForward.collider.tag == "stair") {
				if ((tr.transform.position.y - 1) % 4 == 0) {
					pos += 1 * Vector3.right; 
					pos2 = pos + 1 * Vector3.up + 1 * Vector3.right;
				}
				else if ((tr.transform.position.y - 1) % 4 == 1) {
					pos += 2* Vector3.up + 2 * Vector3.right; 
					pos2 = pos;
				}
				else if ((tr.transform.position.y - 1) % 4 == 3) {
					pos += 1* Vector3.up + 1 * Vector3.right; 
					pos2 = pos + 1 * Vector3.right;
				}
			}
			if (forwardBlocked && hitObjectForward.collider.tag == "stairdown") {
				if ((tr.transform.position.y - 1) % 4 == 1) {
					pos += 1* Vector3.down + 1 * Vector3.right; 
					pos2 = pos + 1 * Vector3.right;
				}
				else if ((tr.transform.position.y - 1) % 4 == 3) {
					pos += 2* Vector3.down + 2 * Vector3.right; 
					pos2 = pos;
				}
				else if ((tr.transform.position.y - 1) % 4 == 0) {
					pos += 1 * Vector3.right; 
					pos2 = pos + 1* Vector3.down + 1 * Vector3.right;
				}
			}
			if (!forwardBlocked) {
				pos += 2 * Vector3.right;
				pos2 = pos;
			}
			newfaceDirection = 0;
		}
		if (Input.GetKey (KeyCode.S) && tr.position == pos2) { // moving back
			if (backBlocked && hitObjectBack.collider.tag == "stair") {
				if ((tr.transform.position.y - 1) % 4 == 0) {
					pos += 1 * Vector3.left; 
					pos2 = pos + 1 * Vector3.up + 1 * Vector3.left;
				}
				else if ((tr.transform.position.y - 1) % 4 == 1) {
					pos += 2* Vector3.up + 2 * Vector3.left; 
					pos2 = pos;
				}
				else if ((tr.transform.position.y - 1) % 4 == 3) {
					pos += 1* Vector3.up + 1 * Vector3.left; 
					pos2 = pos + 1 * Vector3.left;
				}
			}
			if (backBlocked && hitObjectBack.collider.tag == "stairdown") {
				if ((tr.transform.position.y - 1) % 4 == 1) {
					pos += 1* Vector3.down + 1 * Vector3.left; 
					pos2 = pos + 1 * Vector3.left;
				}
				else if ((tr.transform.position.y - 1) % 4 == 3) {
					pos += 2* Vector3.down + 2 * Vector3.left; 
					pos2 = pos;
				}
				else if ((tr.transform.position.y - 1) % 4 == 0) {
					pos += 1 * Vector3.left; 
					pos2 = pos + 1* Vector3.down + 1 * Vector3.left;
				}
			}
			if (!backBlocked) {
				pos += 2 * Vector3.left;
				pos2 = pos;
			}
			newfaceDirection = 2;
		}

		if (Input.GetKey (KeyCode.P) && tr.position == pos) { //temporary vertical up
			pos += 4 * Vector3.up;
			pos2 = pos;
		}

		if (Input.GetKey (KeyCode.O) && tr.position == pos) { // temporary vertical down
			pos += 4 * Vector3.down;
			pos2 = pos;
		}

		moveNormal();

		transform.Rotate (0, 90 * (faceDirection - newfaceDirection), 0); // rotate to face the correct direction

		faceDirection = newfaceDirection;

		if (transform.position == pos2) { // if the character reaches destination, start idle animation
			if (!Input.GetKey (KeyCode.W) && !Input.GetKey (KeyCode.A) && !Input.GetKey (KeyCode.S) && !Input.GetKey (KeyCode.D)) {
				animator.SetInteger ("playermove", 0);
			}
		}
	}

	public void moveNormal () {
		if (Input.GetKey (KeyCode.LeftShift)) { // running
			animator.SetInteger ("playermove", 2); // running animation
			transform.position = Vector3.MoveTowards (transform.position, pos, Time.deltaTime * runspeed);
		} else { // walking
			animator.SetInteger ("playermove", 1); // walking animation
			transform.position = Vector3.MoveTowards (transform.position, pos, Time.deltaTime * walkspeed);
		}

		if (tr.position == pos) {
			pos = pos2;
		}
	}

}

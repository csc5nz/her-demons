using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

	public float walkspeed = 1.8F;
	public float runspeed = 6F;
	public Animator animator;
	public bool stop;
	public bool dmgd;
	public int health = 100;
	public bool canBeHit = true;

	private Vector3 orig;
	private Vector3 pos;
	private Vector3 pos2;
	private Transform tr;
	private int faceDirection; 
	private int newfaceDirection;

	public LayerMask blockingLayer;

	//collider that occupy 2 blocks
	public GameObject colliderPrefab;
	private GameObject colliderNextBlock;
	private GameObject colliderPrevBlock;	

	// Use this for initialization
	void Start () {
		pos = transform.position;
		pos2 = pos;
		tr = transform;
		faceDirection = 0;
		newfaceDirection = 0;
		stop = false;
		dmgd = false;

		//collider that occupy 2 blocks
		colliderNextBlock = Instantiate(colliderPrefab);
		colliderPrevBlock = Instantiate(colliderPrefab);

	}

	// Update is called once per frame
	void Update () {
		move ();
		if (Input.GetMouseButtonDown (0))
			attack ();
	}

	void attack ()
	{
		RaycastHit hit;

		Vector3 curr = tr.position;
		Vector3 currback = curr + 2 * Vector3.back;
		Vector3 currforward = curr + 2 * Vector3.forward;
		Vector3 currright = curr + 2 * Vector3.right;
		Vector3 currleft = curr + 2 * Vector3.left;

		bool blocked;

		if (Input.mousePosition.x < Screen.width / 2 && Input.mousePosition.y > Screen.height / 2) {
			print ("Left Attack");
			newfaceDirection = 1;
			blocked = Physics.Linecast (curr, currforward, out hit, 1 << 9);
			if (blocked) {
				print ("Hit!");
				hit.collider.gameObject.GetComponent<MeeleControl>().getHit() ;
			} else {
				print ("Miss!");
			}
		}
		if (Input.mousePosition.x > Screen.width / 2 && Input.mousePosition.y > Screen.height / 2) {
			print ("Forward Attack");
			newfaceDirection = 4;
			blocked = Physics.Linecast (curr, currright, out hit, 1 << 9);
			if (blocked) {
				print ("Hit!");
				hit.collider.gameObject.GetComponent<MeeleControl>().getHit() ;
			} else {
				print ("Miss!");
			}
		}
		if (Input.mousePosition.x < Screen.width / 2 && Input.mousePosition.y < Screen.height / 2) {
			print ("Back Attack");
			newfaceDirection = 2;
			blocked = Physics.Linecast (curr, currleft, out hit, 1 << 9 );
			if (blocked) {
				print ("Hit!");
				hit.collider.gameObject.GetComponent<MeeleControl>().getHit() ;
			} else {
				print ("Miss!");
			}
		}
		if (Input.mousePosition.x > Screen.width / 2 && Input.mousePosition.y < Screen.height / 2) {
			print ("Right Attack");
			newfaceDirection = 3;
			blocked = Physics.Linecast (curr, currback, out hit, 1 << 9);
			if (blocked) {
				print ("Hit!");
				hit.collider.gameObject.GetComponent<MeeleControl>().getHit() ;
			} else {
				print ("Miss!");
			}
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

		if (leftBlocked && hitObjectLeft.collider.tag == "lever") { // lever activate
			if (Input.GetKeyDown (KeyCode.E)) {
				hitObjectLeft.collider.gameObject.GetComponent <Lever> ().activated = true;
			}
		}
		if (rightBlocked && hitObjectRight.collider.tag == "lever") { // lever activate
			if (Input.GetKeyDown (KeyCode.E)) {
				hitObjectRight.collider.gameObject.GetComponent <Lever> ().activated = true;
			}
		}
		if (backBlocked && hitObjectBack.collider.tag == "lever") { // lever activate
			if (Input.GetKeyDown (KeyCode.E)) {
				hitObjectBack.collider.gameObject.GetComponent <Lever> ().activated = true;
			}
		}
		if (forwardBlocked && hitObjectForward.collider.tag == "lever") { // lever activate
			if (Input.GetKeyDown (KeyCode.E)) {
				hitObjectForward.collider.gameObject.GetComponent <Lever> ().activated = true;
			}
		}

		if (rightBlocked && hitObjectRight.collider.tag == "elevatordown"){ //elevator activate
			if (Input.GetKeyDown (KeyCode.E)) {
				pos += 4 * Vector3.down;
				pos2 = pos;
				hitObjectRight.collider.gameObject.GetComponent <Elevator> ().activated = true;
				stop = true; // while on elevator, player can't issue move commands
			}
		}

		if (rightBlocked && hitObjectRight.collider.tag == "elevatorup"){ //elevator activate
			if (Input.GetKeyDown (KeyCode.E)) {
				pos += 4 * Vector3.up;
				pos2 = pos;
				hitObjectRight.collider.gameObject.GetComponent <Elevator> ().activated = true;
				stop = true; // while on elevator, player can't issue move commands
			}
		}

		if (Input.GetKey (KeyCode.D) && tr.position == pos2 && dmgd == false) { // moving right
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
			if (!rightBlocked || hitObjectRight.collider.tag == "floor") {
				pos += 2 * Vector3.back;
				pos2 = pos;
			}
			newfaceDirection = 3;
			orig = tr.transform.position;
		}
		if (Input.GetKey (KeyCode.A) && tr.position == pos2 && dmgd == false) { // moving left
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
			if (!leftBlocked || hitObjectLeft.collider.tag == "floor") {
				pos += 2 * Vector3.forward;
				pos2 = pos;
			}
			newfaceDirection = 1;
			orig = tr.transform.position;
		}
		if (Input.GetKey (KeyCode.W) && tr.position == pos2 && dmgd == false) { // moving forward
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
			if (!forwardBlocked || hitObjectForward.collider.tag == "floor") {
				pos += 2 * Vector3.right;
				pos2 = pos;
			}
			newfaceDirection = 0;
			orig = tr.transform.position;
		}
		if (Input.GetKey (KeyCode.S) && tr.position == pos2 && dmgd == false) { // moving back
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
			if (!backBlocked || hitObjectBack.collider.tag == "floor") {
				pos += 2 * Vector3.left;
				pos2 = pos;
			}
			newfaceDirection = 2;
			orig = tr.transform.position;
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

		if (stop == true) {
			animator.SetInteger ("playermove", 0);
		}

		if (transform.position == pos2) { // if the character reaches destination, start idle animation
			stop = false;
			orig = tr.transform.position;
			if (!Input.GetKey (KeyCode.W) && !Input.GetKey (KeyCode.A) && !Input.GetKey (KeyCode.S) && !Input.GetKey (KeyCode.D)) {
				animator.SetInteger ("playermove", 0);
			} 
		}
	}

	public void moveNormal () {

		if (colliderNextBlock.transform.position != pos) {
			colliderNextBlock.transform.position = pos;
		}
		if (dmgd == true) {
			transform.position = Vector3.MoveTowards (transform.position, pos, Time.deltaTime * runspeed);
		} else if (Input.GetKey (KeyCode.LeftShift) && stop == false) { // running
			animator.SetInteger ("playermove", 2); // running animation
			transform.position = Vector3.MoveTowards (transform.position, pos, Time.deltaTime * runspeed);
		} else { // walking
			animator.SetInteger ("playermove", 1); // walking animation
			transform.position = Vector3.MoveTowards (transform.position, pos, Time.deltaTime * walkspeed);
		} 

		if (tr.position == pos) {
			pos = pos2;
			colliderPrevBlock.transform.position = transform.position;
		}
	}

	public void damaged(int dmg, int dir, float xval, float zval){
		stop = true;
		dmgd = true;
		animator.SetInteger ("playermove", 3);
		health -= dmg;
		if (dir == 0) {
			if (pos2.x > orig.x) {
				pos = orig;
				pos2 = orig;
			} 
			if (orig.z < zval || orig.z > zval) {
				pos = orig;
				pos2 = orig;
			}
		}
	}
	public void damaged(int dmg){
		stop = true;
		dmgd = true;
		animator.SetInteger ("playermove", 3);
		health -= dmg;
	}
	//public void notDmg(){
		//dmgd = false;
		//animator.SetBool ("hit", false);
	//}
}

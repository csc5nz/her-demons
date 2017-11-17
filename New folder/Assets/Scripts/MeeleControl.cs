using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeeleControl : MonoBehaviour {

	public float walkspeed = 1.8F;
	public float runspeed = 6F;
	public int hp;
	public Animator animator;
	public bool stop;
	public float chaseDist;

	private Vector3 pos;
	private Vector3 pos2;
	private Vector3 home;
	private Transform tr;
	private int faceDirection; 
	private int newfaceDirection;


	public LayerMask blockingLayer;	

	public int direction;
	public GameObject target;

	//navmesh
	public Transform navTarget;
	private  UnityEngine.AI.NavMeshPath navPath;
	private float elapsed = 0.0f;
	private Vector3 navDirection;

	//Lookoout
	public float fieldOfView;
	public float goHomeBufferDistance;
	public float gridDist;
	public Transform navHome;

	//collider that occupy 2 blocks
	public GameObject colliderPrefab;
	private GameObject colliderNextBlock;
	private GameObject colliderPrevBlock;	


	// Use this for initialization
	void Start () {
		pos = transform.position;
		pos2 = pos;
		home = pos;
		tr = transform;
		faceDirection = 0;
		newfaceDirection = 0;
		stop = false;

		direction = 1;

		//navmesh
		navPath = new UnityEngine.AI.NavMeshPath();
		elapsed = 0.0f;

		//collider that occupy 2 blocks
		colliderNextBlock = Instantiate(colliderPrefab);
		colliderPrevBlock = Instantiate(colliderPrefab);

	}

	// Update is called once per frame
	void Update ()
	{
		//navMesh
		// Update the way to the goal every second.
		elapsed += Time.deltaTime;
		if (elapsed > 1.0f) {
			elapsed -= 1.0f;
			UnityEngine.AI.NavMesh.CalculatePath (transform.position, navTarget.position, UnityEngine.AI.NavMesh.AllAreas, navPath);
		}
		for (int i = 0; i < navPath.corners.Length - 1; i++)
			Debug.DrawLine (navPath.corners [i], navPath.corners [i + 1], Color.red);		

			navDirection = navPath.corners [1];
		//print("navDirection: " + navDirection);
		float dist = Vector3.Distance (target.transform.position, home);
		if (dist < chaseDist) {
			chase ();
		}
		if (((Mathf.Abs (transform.position.x - target.transform.position.x) <= 2) && transform.position.z == target.transform.position.z) || 
			((Mathf.Abs (transform.position.z - target.transform.position.z) <= 2) && transform.position.x == target.transform.position.x )){
			if (target.GetComponent<PlayerControl> ().canBeHit) {
				target.GetComponent<PlayerControl> ().canBeHit = false;
				attack ();
				StartCoroutine (timer ());
			}


		}

		if (hp <= 0) {
			gameObject.SetActive (false);
		}
	}

	public void getHit() {
		hp -= 1;
		print (hp);
	}

	private void attack ()
	{
		Vector3 curr = tr.position;
		print ("attack");
		target.GetComponent<PlayerControl> ().damaged (20);


	}

	IEnumerator timer() {
		print(Time.time);
		yield return new WaitForSeconds (2);
		target.GetComponent<PlayerControl> ().canBeHit = true;
		print(Time.time);
	}

	public void chase ()
	{
		float xDif = Mathf.Abs (navDirection.x - transform.position.x);
		float zDif = Mathf.Abs (navDirection.z - transform.position.z);
		//print(navDirection.x+""+transform.position.x);

		if (xDif > zDif) {
			if (navDirection.x < transform.position.x) {
				move2 (3);
			} else if (navDirection.x > transform.position.x) {
				move2 (1);
			}
		} else {
			if (navDirection.z < transform.position.z) {
				move2 (4);
			} else if (navDirection.z > transform.position.z) {
				move2 (2);
			}
		} 
	}
		
	public void move2 (int direction)
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


		//transform.LookAt(target.transform);

		if (direction == 4 && tr.position == pos2) { // moving right
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
		}
		if (direction == 2 && tr.position == pos2) { // moving left
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
		}
		if (direction == 1 && tr.position == pos2) { // moving forward
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
		}
		if (direction == 3 && tr.position == pos2) { // moving back
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
		}
			
		moveNormal();

		transform.Rotate (0, 90 * (faceDirection - newfaceDirection), 0); // rotate to face the correct direction
		faceDirection = newfaceDirection;

		if (stop == true) {
			animator.SetInteger ("enemymove", 0);
		}

		if (transform.position == pos2) { // if the character reaches destination, start idle animation
			stop = false;
			if (((Mathf.Abs (transform.position.x - target.transform.position.x) <= 2) && transform.position.z == target.transform.position.z) || ((Mathf.Abs (transform.position.z - target.transform.position.z) <= 2) && transform.position.x == target.transform.position.x )) { 
				animator.SetInteger ("enemymove", 0);
			}
		}
	}

	public void moveNormal ()
	{
		if (colliderNextBlock.transform.position != pos) {
			colliderNextBlock.transform.position = pos;
		}
			animator.SetInteger ("enemymove", 1); // walking animation
			transform.position = Vector3.MoveTowards (transform.position, pos, Time.deltaTime * walkspeed);
			//if (transform.position == pos)

		if (tr.position == pos) {
			pos = pos2;
			colliderPrevBlock.transform.position = transform.position;
		}
	}

}

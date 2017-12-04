using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour {

	public float walkspeed = 1.8F;
	public float runspeed = 6F;
	public Animator animator;
	public bool stop;
	public bool dmgd;
	public bool attacking;
	public int health;
	public float stamina;
	public bool canBeHit;
	public Image healthBar;
	public Image staminaBar;
	public int hpPotion;
	public GameObject potionimage;
	public Text potionText;

	public Canvas PauseMenu;
	public Canvas Controls;

	public GameObject miniboss;
	public GameObject wall1;
	public GameObject wall2;
	private GameObject wall1inst;
	private GameObject wall2inst;

	private Vector3 orig;
	private Vector3 pos;
	public Vector3 pos2;
	private Transform tr;
	private int faceDirection; 
	private int newfaceDirection;
	private bool paused;

	public AudioSource audio;
	public LayerMask blockingLayer;	

	//collider that occupy 2 blocks
	public GameObject colliderPrefab;
	private GameObject colliderNextBlock;
	private GameObject colliderPrevBlock;	

	// Use this for initialization
	void Start () {
		Time.timeScale = 1;
		audio = GetComponent<AudioSource> ();
		PauseMenu = PauseMenu.GetComponent<Canvas> ();
		PauseMenu.enabled = false;
		Controls = Controls.GetComponent<Canvas> ();
		Controls.enabled = false;
		pos = transform.position;
		pos2 = pos;
		tr = transform;
		faceDirection = 0;
		newfaceDirection = 0;
		stop = false;
		dmgd = false;
		attacking = false;
		canBeHit = true;
		hpPotion = 0;
		potionText.text = "";
		health = 100;
		stamina = 100.0f;

		//collider that occupy 2 blocks
		colliderNextBlock = Instantiate(colliderPrefab);
		colliderPrevBlock = Instantiate(colliderPrefab);
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			if (paused) {
				Time.timeScale = 1;
				PauseMenu.enabled = false;
			} else if (!paused) {
				Time.timeScale = 0;
				PauseMenu.enabled = true;
			}
			paused = !paused;
		}
		if (attacking == false && health > 0) {
			move ();
		}
		
		if (Input.GetMouseButtonDown (0) && tr.position == pos2 && attacking == false && dmgd == false && (stamina > 30) && health > 0) {
			attack ();
		}

		if (Input.GetKeyDown(KeyCode.Q) && attacking == false && dmgd == false && health > 0) {
			drink ();
		}

		if (hpPotion <= 0) {
			potionimage.GetComponent<MeshRenderer> ().enabled = false;
			potionText.text = "";
		} else {
			potionimage.GetComponent<MeshRenderer> ().enabled = true;

		}
		if (stamina < 100 && !attacking) {
			stamina += 0.2f;
		}
		staminaBar.fillAmount = stamina / 100f;

		if (health <= 0) {
			animator.SetInteger ("playermove", 4);

		}

		if (miniboss.GetComponent<MinibossControl> ().hp <= 0) {
			wall1inst.SetActive(false);
			wall2inst.SetActive(false);
		}
	}

	void attack ()
	{
		Vector3 curr = tr.position;
		Vector3 currback = curr + 2.5f * Vector3.back;
		Vector3 currforward = curr + 2.5f * Vector3.forward;
		Vector3 currright = curr + 2.5f * Vector3.right;
		Vector3 currleft = curr + 2.5f * Vector3.left;

		stamina -= 30;

		if (Input.mousePosition.x < Screen.width / 2 && Input.mousePosition.y > Screen.height / 2) { // left attack
			print ("Left Attack");
			newfaceDirection = 1;
			//blocked = Physics.Linecast (curr, currforward, out hit, 1 << 9);
			StartCoroutine(attackdelay (curr, currforward));
		}
		if (Input.mousePosition.x > Screen.width / 2 && Input.mousePosition.y > Screen.height / 2) { // forward attack
			print ("Forward Attack");
			newfaceDirection = 4;
			//blocked = Physics.Linecast (curr, currright, out hit, 1 << 9);
			StartCoroutine(attackdelay (curr, currright));
		}
		if (Input.mousePosition.x < Screen.width / 2 && Input.mousePosition.y < Screen.height / 2) { // back attack
			print ("Back Attack");
			newfaceDirection = 2;
			//blocked = Physics.Linecast (curr, currleft, out hit, 1 << 9 );
			StartCoroutine(attackdelay (curr, currleft));
		}
		if (Input.mousePosition.x > Screen.width / 2 && Input.mousePosition.y < Screen.height / 2) { // right attack
			print ("Right Attack");
			newfaceDirection = 3;
			//blocked = Physics.Linecast (curr, currback, out hit, 1 << 9);
			StartCoroutine(attackdelay (curr, currback)); 
		}
		transform.Rotate (0, 90 * (faceDirection - newfaceDirection), 0); // rotate to face the correct direction
		faceDirection = newfaceDirection;

		animator.SetInteger ("playermove", 5);
		attacking = true;
	}

	IEnumerator attackdelay(Vector3 curr, Vector3 currdest) {
		print(Time.time);
		RaycastHit hit;
		yield return new WaitForSeconds (0.6f);
		bool blocked = Physics.Linecast (curr, currdest, out hit, 1 << 9);
		if (blocked && !dmgd) {
			print ("Hit!");
			if (hit.collider.gameObject.tag == "melee") {
				hit.collider.gameObject.GetComponent<MeeleControl> ().getHit ();
			} else if (hit.collider.gameObject.tag == "archer") {
				hit.collider.gameObject.GetComponent<ArcherController> ().getHit ();
			} else if (hit.collider.gameObject.tag == "miniboss") {
				hit.collider.gameObject.GetComponent<MinibossControl> ().getHit ();
			}
		} else {
			print ("Miss!");
		}
		print(Time.time);
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

		if (transform.position == pos2) { 
			stop = false;
		}

		lever (leftBlocked, hitObjectLeft);
		lever (rightBlocked, hitObjectRight);
		lever (forwardBlocked, hitObjectForward);
		lever (backBlocked, hitObjectBack);

		if (leftBlocked && hitObjectLeft.collider.tag == "End") {
			SceneManager.LoadScene(3);
		}

		if (forwardBlocked && hitObjectForward.collider.tag == "Boss") {
			print ("boss");
			hitObjectForward.collider.gameObject.SetActive (false);
			wall1inst = Instantiate (wall1);
			wall2inst = Instantiate (wall2);
		}


		if (rightBlocked && hitObjectRight.collider.tag == "elevatordown"){ //elevator activate
			elevator(hitObjectRight, Vector3.down);
		}
		if (rightBlocked && hitObjectRight.collider.tag == "elevatorup"){ //elevator activate
			elevator(hitObjectRight, Vector3.up);
		}

		if (Input.GetKey (KeyCode.D) && tr.position == pos2 && dmgd == false && attacking == false) { // moving right
			rightMove(rightBlocked, hitObjectRight);
			newfaceDirection = 3;
			orig = tr.transform.position;
		}
		if (Input.GetKey (KeyCode.A) && tr.position == pos2 && dmgd == false && attacking == false) { // moving left
			leftMove(leftBlocked, hitObjectLeft);
			newfaceDirection = 1;
			orig = tr.transform.position;
		}
		if (Input.GetKey (KeyCode.W) && tr.position == pos2 && dmgd == false && attacking == false) { // moving forward
			forwardMove(forwardBlocked, hitObjectForward);
			newfaceDirection = 0;
			orig = tr.transform.position;
		}
		if (Input.GetKey (KeyCode.S) && tr.position == pos2 && dmgd == false && attacking == false) { // moving back
			backMove(backBlocked, hitObjectBack);
			newfaceDirection = 2;
			orig = tr.transform.position;
		}

		moveNormal();

		transform.Rotate (0, 90 * (faceDirection - newfaceDirection), 0); // rotate to face the correct direction
		faceDirection = newfaceDirection;

		if (stop == true && !dmgd) {
			animator.SetInteger ("playermove", 0);
		}
		if (transform.position == pos2 && !dmgd) { // if the character reaches destination, start idle animation
			stop = false;
			orig = tr.transform.position;
			if (!Input.GetKey (KeyCode.W) && !Input.GetKey (KeyCode.A) && !Input.GetKey (KeyCode.S) && !Input.GetKey (KeyCode.D)) {
				animator.SetInteger ("playermove", 0);
			} 
		}
	}

	public void moveNormal () {
		//Move collider ahead
		if (colliderNextBlock.transform.position != pos) {
			colliderNextBlock.transform.position = pos;
		}
		if (dmgd == true) { //damaged
			transform.position = Vector3.MoveTowards (transform.position, pos, Time.deltaTime * runspeed);
		} else if (Input.GetKey (KeyCode.LeftShift) && stop == false && (stamina > 0)) { // running
			animator.SetInteger ("playermove", 2); // running animation
			transform.position = Vector3.MoveTowards (transform.position, pos, Time.deltaTime * runspeed);
			if (transform.position != pos) {
				stamina -= 0.3f;
			}
		} else { // walking
			animator.SetInteger ("playermove", 1); // walking animation
			transform.position = Vector3.MoveTowards (transform.position, pos, Time.deltaTime * walkspeed);
		} 
		if (tr.position == pos) {
			pos = pos2;
			//Keep collider behind
			colliderPrevBlock.transform.position = transform.position;
		}
	}

	public void damaged(int dmg, int dir, float xval, float zval){ // damaged by archers
		if (health > 0) {
			audio.Play();
		}
		stop = true;
		dmgd = true;
		animator.SetInteger ("playermove", 3);
		health -= dmg;
		healthBar.fillAmount = health / 100f;
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

	public void damaged(int dmg){ // damaged by melee
		if (health > 0) {
			audio.Play ();
		}
		stop = true;
		dmgd = true;
		animator.SetInteger ("playermove", 3);
		health -= dmg;
		healthBar.fillAmount = health / 100f;
	}

	private void drink(){
		if (hpPotion > 0) {
			hpPotion -= 1;
			if (health >= 70) {
				health = 100;
			} else {
				health += 30;
			}
			healthBar.fillAmount = health / 100f;
			potionText.text = "" + hpPotion;
		}
	}

	private void lever(bool blocked, RaycastHit hitObject){ // lever activate
		if (blocked && hitObject.collider.tag == "lever") { 
			if (Input.GetKeyDown (KeyCode.E)) {
				hitObject.collider.gameObject.GetComponent <Lever> ().activated = true;
			}
		}
	}

	private void rightMove(bool blocked, RaycastHit hitObject){ //attempt to move right
		if (blocked && hitObject.collider.tag == "stair") {
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
		if (blocked && hitObject.collider.tag == "stairdown") {
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
		if (!blocked || hitObject.collider.tag == "floor") {
			pos += 2 * Vector3.back;
			pos2 = pos;
		}
	}

	private void leftMove(bool blocked, RaycastHit hitObject){ //attempt to move left
		if (blocked && hitObject.collider.tag == "stair") {
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
		if (blocked && hitObject.collider.tag == "stairdown") {
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
		if (!blocked || hitObject.collider.tag == "floor") {
			pos += 2 * Vector3.forward;
			pos2 = pos;
		}
	}

	private void forwardMove(bool blocked, RaycastHit hitObject){ //attempt to move forward
		if (blocked && hitObject.collider.tag == "stair") {
			if ((tr.transform.position.y - 1) % 4 == 0) {
				pos += 1 * Vector3.right; 
				pos2 = pos + 1 * Vector3.up + 1 * Vector3.right;
			} else if ((tr.transform.position.y - 1) % 4 == 1) {
				pos += 2 * Vector3.up + 2 * Vector3.right; 
				pos2 = pos;
			} else if ((tr.transform.position.y - 1) % 4 == 3) {
				pos += 1 * Vector3.up + 1 * Vector3.right; 
				pos2 = pos + 1 * Vector3.right;
			}
		}
		if (blocked && hitObject.collider.tag == "stairdown") {
			if ((tr.transform.position.y - 1) % 4 == 1) {
				pos += 1 * Vector3.down + 1 * Vector3.right; 
				pos2 = pos + 1 * Vector3.right;
			} else if ((tr.transform.position.y - 1) % 4 == 3) {
				pos += 2 * Vector3.down + 2 * Vector3.right; 
				pos2 = pos;
			} else if ((tr.transform.position.y - 1) % 4 == 0) {
				pos += 1 * Vector3.right; 
				pos2 = pos + 1 * Vector3.down + 1 * Vector3.right;
			}
		}
		if (!blocked || hitObject.collider.tag == "floor") {
			pos += 2 * Vector3.right;
			pos2 = pos;
		}
	}

	private void backMove(bool blocked, RaycastHit hitObject){ // attempt to move back
		if (blocked && hitObject.collider.tag == "stair") {
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
		if (blocked && hitObject.collider.tag == "stairdown") {
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
		if (!blocked || hitObject.collider.tag == "floor") {
			pos += 2 * Vector3.left;
			pos2 = pos;
		}
	}

	private void elevator(RaycastHit hitObject, Vector3 dir){
		if (Input.GetKeyDown (KeyCode.E)) {
			pos += 4 * dir;
			pos2 = pos;
			hitObject.collider.gameObject.GetComponent <Elevator> ().activated = true;
			stop = true; // while on elevator, player can't issue move commands
		}
	}
}

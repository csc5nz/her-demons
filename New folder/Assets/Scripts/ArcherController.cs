using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArcherController : MonoBehaviour {

	public GameObject arrow;
	public float alertdistance;
	public Animator animator;
	public GameObject player;
	public GameObject playerModel;
	public int faceDirection;
	public int hp = 3;
	public bool dmgd;
	public bool dead;
	public Image healthBar;
	public AudioSource audio;

	private RaycastHit hit;
	private int newfaceDirection;

	// Use this for initialization
	void Start () {
		audio = GetComponent<AudioSource> ();
		dead = false;
		dmgd = false;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 playerpos = player.transform.position;
		Vector3 enemypos = transform.position;
		float distance = Vector3.Distance(playerpos,enemypos);

		RaycastHit hit;
		if (playerpos.y == enemypos.y && distance <= alertdistance && !dmgd) { 
			animator.SetInteger ("alert", 1);
		} else if (!dmgd){
			animator.SetInteger ("alert", 0);
		}

		if (playerpos.y == enemypos.y && distance <= alertdistance && !dead && !dmgd) { //must be from same level to recognize player and distance between enemy and player must be less than 10 tiles away
			if (playerpos.x == enemypos.x) {
				if (playerpos.z > enemypos.z) { //player at top left
					bool blocked = Physics.Linecast (enemypos, playerpos, out hit, 1 << 8);
					if(hit.collider.gameObject == playerModel){
						animator.SetInteger ("alert", 2);
					}
					newfaceDirection = 3;
				} else { // player at bottom right
					bool blocked = Physics.Linecast (enemypos, playerpos, out hit, 1 << 8);
					if(hit.collider.gameObject == playerModel){
						animator.SetInteger ("alert", 2);
					}
					newfaceDirection = 1;
				}
			}
			else if (playerpos.z == enemypos.z) {
				if (playerpos.x > enemypos.x) { // player at top right
					bool blocked = Physics.Linecast (enemypos, playerpos, out hit, 1 << 8);
					if(hit.collider.gameObject == playerModel){
						animator.SetInteger ("alert", 2);
					}
					newfaceDirection = 2;
				} else { // player at bottom left
					bool blocked = Physics.Linecast (enemypos, playerpos, out hit, 1 << 8);
					if (blocked) {
						if (hit.collider.gameObject == playerModel) {
							animator.SetInteger ("alert", 2);
						}
					}
					newfaceDirection = 0;
				}
			}
			if (Mathf.Abs (playerpos.x - enemypos.x) >= Mathf.Abs (playerpos.z - enemypos.z)) { // if distance btwn x is greater than distance btwn z
				if (playerpos.x > enemypos.x) { //if player is behind enemy
					newfaceDirection = 2;
				} else {
					newfaceDirection = 0;
				}
			} else { // if distance btwn z is greater than distance btwn x
				if (playerpos.z > enemypos.z) { //if player is left of enemy
					newfaceDirection = 3;
				} else {
					newfaceDirection = 1;
				}
			}
		}

		transform.Rotate (0, 90 * (faceDirection - newfaceDirection), 0);
		faceDirection = newfaceDirection;

		if (hp <= 0) {
			animator.SetInteger("alert", 4);
		}
	}
	
	public void Shoot(){
		Vector3 arrowpos = transform.position;
		int dir = 0;

		if (faceDirection == 0) { //bottom left
			arrowpos += 2.0f * Vector3.left + 2.4f * Vector3.up;
			dir = 0;

		} else if (faceDirection == 1) { // bottom right
			arrowpos += 2.0f * Vector3.back + 2.4f * Vector3.up;
			dir = 1;
		} else if (faceDirection == 2) { //top right
			arrowpos += 2.0f * Vector3.right + 2.4f * Vector3.up;
			dir = 2;
		} else if (faceDirection == 3) { // top left
			arrowpos += 2.0f * Vector3.forward + 2.4f * Vector3.up;
			dir = 3;
		}

		GameObject arrowinstance = Instantiate(arrow, arrowpos, Quaternion.identity); 

		if (dir == 1 || dir == 3) {
			arrowinstance.transform.Rotate (0, 90, 0);
		}

		arrowinstance.GetComponent<Arrow> ().archer = this.gameObject;
		arrowinstance.GetComponent<Arrow> ().direction = dir;
	}

	public void getHit() {
		audio.Play ();
		hp -= 1;
		healthBar.fillAmount = hp / 3f;
		dmgd = true;
		animator.SetInteger ("alert", 5);

	}

	public void notDmg(){
		dmgd = false;
	}

	public void Death(){
		//player.GetComponent<PlayerControl>().attacking = false;
		this.gameObject.SetActive(false);
	}
}

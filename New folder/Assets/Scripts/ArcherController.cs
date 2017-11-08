using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherController : MonoBehaviour {

	public float alertdistance;
	public Animator animator;
	public GameObject player;
	public GameObject playerModel;
	public int state; // 0 is idle, 1 is alert
	public RaycastHit hit;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 playerpos = player.transform.position;
		Vector3 enemypos = transform.position;
		float distance = Vector3.Distance(playerpos,enemypos);

		RaycastHit hit;
		if (playerpos.y == enemypos.y && distance <= alertdistance) { 
			animator.SetInteger ("alert", 1);
		} else {
			animator.SetInteger ("alert", 0);
		}

		if (playerpos.y == enemypos.y && distance <= alertdistance) { //must be from same level to recognize player and distance between enemy and player must be less than 10 tiles away
			if (playerpos.x == enemypos.x) {
				if (playerpos.z > enemypos.z) { //player at left
					bool blocked = Physics.Linecast (enemypos, playerpos, out hit, 1 << 8);
					if(hit.collider.gameObject == playerModel){
						animator.SetInteger ("alert", 2);
					}
				} else { // player at right
					bool blocked = Physics.Linecast (enemypos, playerpos, out hit, 1 << 8);
					if(hit.collider.gameObject == playerModel){
						animator.SetInteger ("alert", 2);
					}
				}
			}
			else if (playerpos.z == enemypos.z) {
				if (playerpos.x > enemypos.x) { // player at back
					bool blocked = Physics.Linecast (enemypos, playerpos, out hit, 1 << 8);
					if(hit.collider.gameObject == playerModel){
						animator.SetInteger ("alert", 2);
					}
				} else { // player at front
					bool blocked = Physics.Linecast (enemypos, playerpos, out hit, 1 << 8);
					if (blocked) {
						if (hit.collider.gameObject == playerModel) {
							animator.SetInteger ("alert", 2);
						}
					}
				}
			}
		}
	}
	
	public void shoot(Vector3 origin, Vector3 Direction){
		animator.SetInteger ("alert", 2);
	}


}

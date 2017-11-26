using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Modelhit : MonoBehaviour {

	public GameObject player;

	public void notDmg(){ //At the end of the damaged animation, set status back to normal and animation to idle
		player.GetComponent<PlayerControl>().dmgd = false;
		//player.GetComponent<PlayerControl>().attacking = false;
		player.GetComponent<PlayerControl> ().animator.SetInteger ("playermove", 0);
	}

	public void stopAttacking(){ //at the end of the attack animation, set status back to normal and animation to idle
		player.GetComponent<PlayerControl>().attacking = false;
		player.GetComponent<PlayerControl>().animator.SetInteger ("playermove", 0);
	}

	public void attack(bool a, RaycastHit hit){
		if (a = true) {
			print ("Hit!");
			if (hit.collider.gameObject.tag == "melee") {
				hit.collider.gameObject.GetComponent<MeeleControl> ().getHit ();
			} else if (hit.collider.gameObject.tag == "archer") {
				hit.collider.gameObject.GetComponent<ArcherController> ().getHit ();
			}
		}
	}
}

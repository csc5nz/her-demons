using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeeleModelhit : MonoBehaviour {

	public GameObject meele;

	public void notDmg(){ //At the end of the damaged animation, set status back to normal and animation to idle
		meele.GetComponent<MeeleControl>().dmg = false;
		//player.GetComponent<PlayerControl>().attacking = false;
		meele.GetComponent<MeeleControl> ().animator.SetInteger ("playermove", 0);
	}

	public void stopAttacking(){ //at the end of the attack animation, set status back to normal and animation to idle
		meele.GetComponent<MeeleControl>().attacking = false;
		meele.GetComponent<MeeleControl>().animator.SetInteger ("playermove", 0);
	}
}

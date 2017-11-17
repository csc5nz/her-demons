using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Modelhit : MonoBehaviour {

	public GameObject player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void notDmg(){
		player.GetComponent<PlayerControl>().dmgd = false;
		//player.GetComponent<PlayerControl>().attacking = false;
		player.GetComponent<PlayerControl> ().animator.SetInteger ("playermove", 0);
	}

	public void stopAttacking(){
		player.GetComponent<PlayerControl>().attacking = false;
		player.GetComponent<PlayerControl>().animator.SetInteger ("playermove", 0);
	}
}

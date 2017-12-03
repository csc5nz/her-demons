using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : MonoBehaviour {

	public GameObject player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (player.transform.position == transform.position) {
			player.GetComponent<PlayerControl> ().hpPotion += 1;
			player.GetComponent<PlayerControl> ().potionText.text = "" + player.GetComponent<PlayerControl> ().hpPotion;
			this.gameObject.SetActive (false);
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisRender : MonoBehaviour {

	public GameObject player;
	public float playerY;
	public float playerX;
	public float playerZ;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (player.transform.position.y != playerY || !(player.transform.position.z < playerZ) || !(player.transform.position.x < playerX)) {
			this.gameObject.GetComponent<Renderer> ().enabled = false;
		} else {
			this.gameObject.GetComponent<Renderer>().enabled = true;
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastBox : MonoBehaviour {
	public GameObject player;

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		if (player.transform.position != player.GetComponent<PlayerControl> ().pos2) {
			GetComponent <MeshRenderer>().enabled = false;
		} else {
			Color tempColor = this.gameObject.GetComponent<Renderer> ().material.color;
			GetComponent <MeshRenderer>().enabled = true;
		}

		if (Input.mousePosition.x < Screen.width / 2 && Input.mousePosition.y > Screen.height / 2) { // left attack
			transform.position = player.transform.position + 2*Vector3.forward + 0.001f*Vector3.up;
		}
		if (Input.mousePosition.x > Screen.width / 2 && Input.mousePosition.y > Screen.height / 2) { // forward attack
			transform.position = player.transform.position + 2*Vector3.right + 0.001f*Vector3.up;
		}
		if (Input.mousePosition.x < Screen.width / 2 && Input.mousePosition.y < Screen.height / 2) { // back attack
			transform.position = player.transform.position + 2*Vector3.left + 0.001f*Vector3.up;
		}
		if (Input.mousePosition.x > Screen.width / 2 && Input.mousePosition.y < Screen.height / 2) { // right attack
			transform.position = player.transform.position + 2*Vector3.back + 0.001f*Vector3.up;
		}
	}
}

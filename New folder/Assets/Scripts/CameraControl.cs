using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

	public GameObject player;
	public bool hpbar = false;

	public float cameraX = -75.0f;
	public float cameraY = 75.0f;
	public float cameraZ = -75.0f;

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		Vector3 pos = player.transform.position;
		pos.x += cameraX;
		pos.y += cameraY;
		pos.z += cameraZ;

		transform.position = pos;
		if (hpbar == true) {
			if (player.activeInHierarchy == false) {
				this.gameObject.SetActive (false);
			}
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controlsScript : MonoBehaviour {
	public Canvas controls;
	// Update is called once per frame

	void Start() {
		controls = controls.GetComponent<Canvas> ();
	}
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			controls.enabled = false;

		}
	}

}
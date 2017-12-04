using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsScript : MonoBehaviour {
	public Canvas credits;
	// Update is called once per frame

	void Start() {
		credits = credits.GetComponent<Canvas> ();
	}
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			credits.enabled = false;

	}
}

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour {

	public string levertype;
	public bool activated;
	public bool state;
	public GameObject rotatingplatform;
	public GameObject handle;

	// Use this for initialization
	void Start () {
		activated = false;
		state = false;

	}

	// Update is called once per frame
	void Update () {
		if (activated == true && state == false) {
			rotatingplatform.transform.Rotate (0, -30*Time.deltaTime, 0);
			if ((rotatingplatform.transform.rotation.eulerAngles.y) <= 90) {
				activated = false;
				state = true;
			}
			handle.transform.eulerAngles = Vector3.Lerp (handle.transform.rotation.eulerAngles, new Vector3 (90, 180, 0), Time.deltaTime);
		}
		if (activated == true && state == true) {
			rotatingplatform.transform.Rotate (0, 30*Time.deltaTime, 0);
		
			if (rotatingplatform.transform.rotation.eulerAngles.y >= 180) {
				activated = false;
				state = false;
			}
			handle.transform.eulerAngles = Vector3.Lerp (handle.transform.rotation.eulerAngles, new Vector3 (0, 180, 0), Time.deltaTime);
		}
	}
}

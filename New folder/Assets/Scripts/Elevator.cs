using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour {

	public GameObject platform;
	public bool activated;

	public Vector3 pos;
	public Vector3 pos2;

	// Use this for initialization
	void Start () {
		pos = platform.transform.position;
		pos2 = platform.transform.position + 4 * Vector3.down;
		activated = false;
	}

	// Update is called once per frame
	void Update () {
		if (tag == "elevatordown") {
			if (activated == true) {
				platform.transform.position = Vector3.MoveTowards (platform.transform.position, pos2, Time.deltaTime * 1.8F);
			}
			if (platform.transform.position == pos2) {
				activated = false;
			}
		}
		if (tag == "elevatorup") {
			if (activated == true) {
				platform.transform.position = Vector3.MoveTowards (platform.transform.position, pos, Time.deltaTime * 1.8F);
			}
			if (platform.transform.position == pos) {
				activated = false;
			}
		}
	}
}

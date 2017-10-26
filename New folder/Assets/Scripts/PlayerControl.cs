using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

	public float speed = 2.5F;
	public string facing;

	private Vector3 pos;
	private Transform tr;
	private bool movingX = false;
	private bool movingZ = false;

	// Use this for initialization
	void Start () {
		pos = transform.position;
		tr = transform;
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKey (KeyCode.D) && tr.position == pos) {
			pos += 2*Vector3.back;
		}
		if (Input.GetKey (KeyCode.A) && tr.position == pos) {
			pos += 2*Vector3.forward;
		}
		if (Input.GetKey (KeyCode.W) && tr.position == pos) {
			pos += 2*Vector3.right;
		}
		if (Input.GetKey (KeyCode.S) && tr.position == pos) {
			pos += 2*Vector3.left;
		}

		transform.position = Vector3.MoveTowards (transform.position, pos, Time.deltaTime * speed);

	}
}

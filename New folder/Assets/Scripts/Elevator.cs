using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour {

	public GameObject player;
	public bool up;
	public bool move;
	private float dest;

	// Use this for initialization
	void Start () {
		dest = transform.position.y + 4;
	}

	// Update is called once per frame
	void Update () {

		if (transform.position == player.transform.position) {
		}
	}
}

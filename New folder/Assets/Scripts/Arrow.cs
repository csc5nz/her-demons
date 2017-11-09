using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

	public GameObject archer;
	public GameObject player;
	public int direction;
	public int arrowspeed;

	private Vector3 pos;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		pos = transform.position;
	}

	// Update is called once per frame
	void Update () {
		if (direction == 0) {
			transform.position = Vector3.MoveTowards (transform.position, pos + 30*Vector3.left, Time.deltaTime * arrowspeed);
			if (transform.position == pos + 30 * Vector3.left) {
				Destroy (this.gameObject);
			}
		} else if (direction == 1) {
			transform.position = Vector3.MoveTowards (transform.position, pos + 30*Vector3.back, Time.deltaTime * arrowspeed);
			if (transform.position == pos + 30 * Vector3.left) {
				Destroy (this.gameObject);
			}
		} else if (direction == 2) {
			transform.position = Vector3.MoveTowards (transform.position, pos + 30*Vector3.right, Time.deltaTime * arrowspeed);
			if (transform.position == pos + 30 * Vector3.left) {
				Destroy (this.gameObject);
			}
		} else if (direction == 3) {
			transform.position = Vector3.MoveTowards (transform.position, pos + 30*Vector3.forward, Time.deltaTime * arrowspeed);
			if (transform.position == pos + 30 * Vector3.left) {
				Destroy (this.gameObject);
			}
		}
	
		if(Mathf.Abs(transform.position.x - player.transform.position.x) > 0.5 && Mathf.Abs(transform.position.z - player.transform.position.z) > 0.5){ 
			player.GetComponent<PlayerControl>().health -= 20;
			Destroy (this.gameObject);
		}
	}
}

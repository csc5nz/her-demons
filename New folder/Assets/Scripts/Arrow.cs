using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {
	public GameObject archer;
	public GameObject player;
	public GameObject hitarea;
	public int direction;
	public int arrowspeed;

	public GameObject hitareainstance;
	private Vector3 hitareapos;
	private Vector3 pos;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		pos = transform.position;
		//hitareapos = transform.position + 2.4f * Vector3.down;
		//hitareainstance = Instantiate (hitarea, hitareapos, Quaternion.identity);
	}

	// Update is called once per frame
	void Update () {
		if (direction == 0) { // bottom left
			transform.position = Vector3.MoveTowards (transform.position, pos + 30.0f*Vector3.left, Time.deltaTime * arrowspeed);
			if (transform.position == pos + 30 * Vector3.left) {
				Destroy (this.gameObject);
			}
			//if ((transform.position.x - pos.x) % 2 < Mathf.Epsilon) { // if arrow is even distance away from the archer
				//Vector3 newpos = transform.position + 2.4f*Vector3.down;
				//hitareainstance.transform.position = newpos;
			//}

		} else if (direction == 1) { // bottom right
			transform.position = Vector3.MoveTowards (transform.position, pos + 30.0f*Vector3.back, Time.deltaTime * arrowspeed);
			if (transform.position == pos + 30 * Vector3.back) {
				Destroy (this.gameObject);
			}
		} else if (direction == 2) { // top right
			transform.position = Vector3.MoveTowards (transform.position, pos + 30.0f*Vector3.right, Time.deltaTime * arrowspeed);
			if (transform.position == pos + 30 * Vector3.right) {
				Destroy (this.gameObject);
			}
		} else if (direction == 3) { // top left
			transform.position = Vector3.MoveTowards (transform.position, pos + 30.0f*Vector3.forward, Time.deltaTime * arrowspeed);
			if (transform.position == pos + 30 * Vector3.forward) {
				Destroy (this.gameObject);
			}
		}
	
		if(Mathf.Abs(transform.position.x - player.transform.position.x) < 1.0f && Mathf.Abs(transform.position.z - player.transform.position.z) < 1.0f){ 
			player.GetComponent<PlayerControl>().damaged(20, direction, transform.position.x, transform.position.z);
			Destroy (this.gameObject);
		}
	}
}

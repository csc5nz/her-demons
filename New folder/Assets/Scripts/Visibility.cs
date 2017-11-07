using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Visibility : MonoBehaviour {

	public GameObject player;
	public bool hit;
	public bool hit2;
	public bool hit3;

	private Vector3 two;
	private Vector3 three;
	private RaycastHit hitObject;
	private RaycastHit hitObject2;
	private RaycastHit hitObject3;

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		two = player.transform.position + 2.5f * Vector3.up;
		three = player.transform.position + 1.25f * Vector3.up;
		hit = Physics.Raycast (player.transform.position, new Vector3 (-75.0f, 75.0f, -75.0f), out hitObject, 50.0f);
		hit2 = Physics.Raycast (two, new Vector3 (-75.0f, 75.0f, -75.0f), out hitObject2, 50.0f);
		hit2 = Physics.Raycast (three, new Vector3 (-75.0f, 75.0f, -75.0f), out hitObject3, 50.0f);

		if (hit) {
			if (hitObject.collider.gameObject == this.gameObject || hitObject2.collider.gameObject == this.gameObject || hitObject3.collider.gameObject == this.gameObject){
				this.gameObject.GetComponent<Renderer> ().material.shader = Shader.Find ("Transparent/Diffuse");
				Color tempColor = this.gameObject.GetComponent<Renderer> ().material.color;
				tempColor.a = 0.3F;
				this.gameObject.GetComponent<Renderer> ().material.color = tempColor;
			} else {
				this.gameObject.GetComponent<Renderer> ().material.shader = Shader.Find ("Standard");
				Color tempColor = this.gameObject.GetComponent<Renderer> ().material.color;
				tempColor.a = 1.0F;
				this.gameObject.GetComponent<Renderer> ().material.color = tempColor;
			}

		} else {
			this.gameObject.GetComponent<Renderer> ().material.shader = Shader.Find ("Standard");
			Color tempColor = this.gameObject.GetComponent<Renderer> ().material.color;
			tempColor.a = 1.0F;
			this.gameObject.GetComponent<Renderer> ().material.color = tempColor;
		}
	}
}

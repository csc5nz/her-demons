using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Visibility : MonoBehaviour {

	public GameObject player;
	public float alphalevel;

	private bool skip1;
	private bool skip2;
	private bool skip3;
	private Vector3 two;
	private Vector3 three;
	private RaycastHit hitObject;
	private RaycastHit hitObject2;
	private RaycastHit hitObject3;
	private Vector3 direction;

	// Use this for initialization
	void Start () {
		direction = new Vector3 (-1.0f, 1.0f, -1.0f);
	}

	// Update is called once per frame
	void Update () {
		skip1 = false;
		skip2 = false;
		skip3 = false;
		two = player.transform.position + 2.5f * Vector3.up;
		three = player.transform.position + 1.25f * Vector3.up;
		bool hit = Physics.Raycast (player.transform.position, direction, out hitObject, 50.0f);
		bool hit2 = Physics.Raycast (two, direction, out hitObject2, 50.0f);
		bool hit3 = Physics.Raycast (three, direction, out hitObject3, 50.0f);

		if (hit == true) {
			if (hitObject.collider.gameObject == this.gameObject){
				this.gameObject.GetComponent<Renderer> ().material.shader = Shader.Find ("Transparent/Diffuse");
				Color tempColor = this.gameObject.GetComponent<Renderer> ().material.color;
				tempColor.a = alphalevel;
				this.gameObject.GetComponent<Renderer> ().material.color = tempColor;
				skip1 = true;
			}
		} 

		if (hit2 == true) {
			if (hitObject2.collider.gameObject == this.gameObject){
				this.gameObject.GetComponent<Renderer> ().material.shader = Shader.Find ("Transparent/Diffuse");
				Color tempColor = this.gameObject.GetComponent<Renderer> ().material.color;
				tempColor.a = alphalevel;
				this.gameObject.GetComponent<Renderer> ().material.color = tempColor;
				skip2 = true;
			} 
		} 
		if (hit3 == true) {
			if (hitObject3.collider.gameObject == this.gameObject){
				this.gameObject.GetComponent<Renderer> ().material.shader = Shader.Find ("Transparent/Diffuse");
				Color tempColor = this.gameObject.GetComponent<Renderer> ().material.color;
				tempColor.a = alphalevel;
				this.gameObject.GetComponent<Renderer> ().material.color = tempColor;
				skip3 = true;
			} 
		} 
		if(skip1 == false && skip2 == false && skip3 == false){
			this.gameObject.GetComponent<Renderer> ().material.shader = Shader.Find ("Standard");
			Color tempColor = this.gameObject.GetComponent<Renderer> ().material.color;
			tempColor.a = 1.0F;
			this.gameObject.GetComponent<Renderer> ().material.color = tempColor;
		}
			
	}
}

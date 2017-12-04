using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisibilityAssociated : MonoBehaviour {

	public GameObject vis;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (vis.GetComponent<Renderer> ().material.shader == Shader.Find ("Transparent/Diffuse")) {
			this.gameObject.GetComponent<Renderer> ().material.shader = Shader.Find ("Transparent/Diffuse");
			Color tempColor = this.gameObject.GetComponent<Renderer> ().material.color;
			tempColor.a = 0.1F;
			this.gameObject.GetComponent<Renderer> ().material.color = tempColor;
		}
		if (vis.GetComponent<Renderer> ().material.shader == Shader.Find ("Standard")) {
			this.gameObject.GetComponent<Renderer> ().material.shader = Shader.Find ("Standard");
			Color tempColor = this.gameObject.GetComponent<Renderer> ().material.color;
			tempColor.a = 0.1F;
			this.gameObject.GetComponent<Renderer> ().material.color = tempColor;
		}
	}
}

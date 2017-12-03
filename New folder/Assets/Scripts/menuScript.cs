using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class menuScript : MonoBehaviour {

	public Button start;
	public Button controls;
	public Button credits;
	public Button exit;
	public Canvas ctrlscreen;
	public Button back;

	// Use this for initialization
	void Start () {
		ctrlscreen.GetComponent<Canvas>();
		ctrlscreen.enabled = false;

		start = start.GetComponent<Button> ();
		start.onClick.AddListener (StartGame);

		controls = controls.GetComponent<Button> ();
		controls.onClick.AddListener (ControlScreen);

		credits = credits.GetComponent<Button> ();

		exit = exit.GetComponent<Button> ();
		exit.onClick.AddListener (ExitGame);

		back = back.GetComponent<Button> ();
		back.onClick.AddListener (backtomenu);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ControlScreen() {
		ctrlscreen.enabled = true; 
	}

	public void StartGame() {
		SceneManager.LoadScene (1);
	}

	public void ExitGame() {
		Application.Quit ();
	}

	public void backtomenu() {
		ctrlscreen.enabled = false;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour {
	public Button Controls;
	public Button Menu;
	public Canvas PauseMenu;
	public Canvas ControlScreen;

	// Use this for initialization
	void Start () {
		Controls = Controls.GetComponent<Button> ();
		Menu = Menu.GetComponent<Button> ();
		PauseMenu = PauseMenu.GetComponent<Canvas> ();
		Controls.onClick.AddListener (controlScr);
		Menu.onClick.AddListener (MainMenu);
	}

	void controlScr() {
		ControlScreen.enabled = true;
	}

	void MainMenu() {
		SceneManager.LoadScene (0);
	}

}


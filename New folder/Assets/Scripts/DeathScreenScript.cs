using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeathScreenScript : MonoBehaviour {

	public Button restart;
	public Button menu;

	// Use this for initialization
	void Start () {
		restart.GetComponent<Button> ();
		restart.onClick.AddListener (RestartGame);

		menu.GetComponent<Button> ();
		menu.onClick.AddListener (ToMenu);
		
	}

	void RestartGame() {
		SceneManager.LoadScene (1);
		Debug.Log ("HI");
	}

	void ToMenu() {
		SceneManager.LoadScene (0);
		Debug.Log ("HI");
	}

}

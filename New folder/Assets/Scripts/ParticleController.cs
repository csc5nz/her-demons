using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour {
	public GameObject player;
	public ParticleSystem particleSystem;
	private int prevHpPotion; 


	void Awake ()
	{
		var main = particleSystem.main;
		main.loop = true;
		main.playOnAwake = false;
	}

	void Start ()
	{	
		particleSystem.enableEmission = false;
		prevHpPotion = player.GetComponent<PlayerControl>().hpPotion;
		transform.position = player.transform.position;
		transform.parent = player.transform ;


	}

	// Update is called once per frame
	void Update () {
		print("prev" + prevHpPotion);
		print("hp" + player.GetComponent<PlayerControl>().hpPotion);

		if (player.GetComponent<PlayerControl>().hpPotion < prevHpPotion) {
			print("if statement");
			StartCoroutine(HpPotionEffect());
			//prevHpPotion = player.GetComponent<PlayerControl> ().hpPotion;
		}
		prevHpPotion = player.GetComponent<PlayerControl> ().hpPotion;
	}

	IEnumerator HpPotionEffect() {
		print(Time.time);
		particleSystem.enableEmission = true;
		yield return new WaitForSeconds (1.0f);
		particleSystem.enableEmission = false;
		print(Time.time);
	}
}


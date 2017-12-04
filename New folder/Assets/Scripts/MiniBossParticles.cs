using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBossParticles : MonoBehaviour {
	public GameObject miniboss;
	public ParticleSystem particleSystem;
	private int prevAnimatorInt; 

	void Awake ()
	{
		var main = particleSystem.main;
		main.loop = true;
		main.playOnAwake = false;
	}

	void Start ()
	{	
		particleSystem.enableEmission = false;
		prevAnimatorInt = miniboss.GetComponent<MinibossControl>().animator.GetInteger("enemymove");
		transform.position = miniboss.transform.position;
		transform.parent = miniboss.transform ;
	}

	// Update is called once per frame
	void Update () {
		//print("prev" + prevHpPotion);
		//print("hp" + miniboss.GetComponent<MinibossControl>().hpPotion);
		if (miniboss.GetComponent<MinibossControl>().particles == false) {
			print("if statement");
			StartCoroutine(attackEffect());
			//prevHpPotion = player.GetComponent<PlayerControl> ().hpPotion;
		}
	}

	IEnumerator attackEffect() {
		print(Time.time);
		particleSystem.enableEmission = true;
		yield return new WaitForSeconds (.5f);
		particleSystem.enableEmission = false;
		print(Time.time);
	}
}

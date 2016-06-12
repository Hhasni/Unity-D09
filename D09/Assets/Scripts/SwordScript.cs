using UnityEngine;
using System.Collections;

public class SwordScript : MonoBehaviour {
	
	public Animator player_anim;
	private CharacterScript Splayer;
	// Use this for initialization
	void Awake () {
		Splayer = GameObject.FindGameObjectWithTag ("Player").GetComponent<CharacterScript> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider hit){
		if (player_anim.GetBool ("Attack") == true && hit.gameObject.tag == "enemy" && Splayer.gTarget && hit.gameObject.GetInstanceID() == Splayer.gTarget.GetInstanceID())
			Splayer.Attack ();
	}
}

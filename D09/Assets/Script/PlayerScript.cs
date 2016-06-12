using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour {
	
	public int 			HP;
	public int 			Damage;
	public bool 		isDead;
	public UnityStandardAssets.Characters.FirstPerson.FirstPersonController pscs;
	private	ParticleSystem	ps;
	// Use this for initialization
	void Start () {
		HP = 100;
		pscs = GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController> ();
		ps = GetComponentInChildren<ParticleSystem> ();
	}

	public void freez(){
		pscs.enabled = false;
		ps.Play ();
		Invoke ("unfreez", 2f);
	}

	public void unfreez(){
		
		ps.Stop ();
		pscs.enabled = true;
	}

	public void getHit(int dam){
		if (HP > 0) {
			HP -= dam;
			if (HP <= 0) {
				HP = 0;
				isDead = true;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (isDead){
			if (pscs.enabled)
				pscs.enabled = false;
			return;
		}
	}
}

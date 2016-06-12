using UnityEngine;
using System.Collections;

public class EnergyBallScript : MonoBehaviour {
	
	private Rigidbody			Rb;
	public  bool 				isShooted;
	public	GameObject			Player;
	public	PlayerScript		sPlayer;
	public	int					Speed;
	public	float				ftime;

	// Use this for initialization
	void Start () {
		Player = GameObject.FindGameObjectWithTag ("Player").gameObject;
		sPlayer = Player.GetComponent<PlayerScript> ();
		Rb = GetComponent<Rigidbody> ();
	}

	void OnTriggerEnter(Collider col){
		if (col.tag == "Player") {
			Debug.Log (col.tag);
			sPlayer.freez();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (!isShooted) {
			transform.LookAt (Player.transform.position);
			Rb.AddForce (transform.forward * 400);
			isShooted = true;
		}
		if (ftime > 10) {
			ftime += Time.deltaTime;
			Destroy(this.gameObject);
		}
	}
}

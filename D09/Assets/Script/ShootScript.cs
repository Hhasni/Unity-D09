using UnityEngine;
using System.Collections;

public class ShootScript : MonoBehaviour {
	
	public 	GameObject 		bullet;
	public 	float 			time;
	public 	float 			rps;
	private AudioSource	 	my_audio;
	public 	bool 			active;
	private PlayerScript	sPlayer;	

	// Use this for initialization
	void Start () {
		sPlayer = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerScript> ();
		my_audio = GetComponent<AudioSource> ();
		active = true;
	}

	void ft_shoot(){
		time += Time.deltaTime;
		if (Input.GetMouseButton (0) && time > rps) {
			my_audio.Play ();
			time = 0;
			Instantiate(bullet, transform.GetChild(0).position, transform.root.GetChild(0).rotation);
		}
	}
	// Update is called once per frame
	void Update () {
		if (active == true && !sPlayer.isDead)
			ft_shoot ();
	}
}

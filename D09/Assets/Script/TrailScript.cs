using UnityEngine;
using System.Collections;

public class TrailScript : MonoBehaviour {

	private	LineRenderer		line_r;
//	public 	Transform 			GunPoint;
	public	Ray 				ray;
	public	int					i;
	public  float				moveSpeed;
	private Rigidbody			Rb;
	public  bool 				isShooted;
	public 	GameObject 			smoke;
	public	PlayerScript		sPlayer;
	public	int					Speed;
	public	enum				Type{normal, zone};
	public	Type				type;
	
	// Use this for initialization
	void Start () {

		sPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
//		GunPoint = GameObject.FindGameObjectWithTag ("gun").transform.GetChild(0);
		Rb = GetComponent<Rigidbody> ();
	}
	void OnTriggerEnter(Collider col){
		if (col.gameObject.tag == "enemy") {
			col.gameObject.transform.root.gameObject.GetComponent<ZombitchScript> ().GetHit (sPlayer.Damage);
			if (type == Type.zone)
				Instantiate (smoke, this.transform.position, Quaternion.identity);
			Destroy (this.gameObject);
		}
		else if (col.gameObject.tag == "map"){
			RaycastHit[] hits;
			hits = Physics.RaycastAll (transform.position, transform.forward, Mathf.Infinity);
			foreach (RaycastHit hit in hits) {
				if (hit.transform.tag == "map")
					Instantiate (smoke, hit.point, Quaternion.identity);
			}
			Destroy (this.gameObject);
		}
	}

	// Update is called once per frame
	void Update () {
		if (!isShooted) {
			Rb.AddForce (transform.forward * Speed);
			isShooted = true;
		}
	}
}

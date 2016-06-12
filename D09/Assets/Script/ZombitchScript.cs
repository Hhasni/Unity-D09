using UnityEngine;
using System.Collections;

public class ZombitchScript : MonoBehaviour {
	
	public	Vector3			TargetPos;
	public	GameObject		Target;
	private	GameObject		Player;
	private NavMeshAgent	my_Nma;
	private	Rigidbody		my_Rbdy;
	private	CapsuleCollider	my_col;
	public 	bool 			IsReach;
	public	Animator		my_Anim;
	public	int				HP;
	public	bool			isDead;
	private int				DetectZone;	
	public	bool			IsAttackable;
	public  int 			Damage;
	public  int 			Speed;
	private float 			ftime;
	public float 			fhadokentime;
	public float 			nextHadoken;
	public 	GameObject 		hadoken;
	private SpawnerManager	MSpawner;
	public 	float			factor;
	public	enum			Type{Boss, Zombitch};
	public	Type			type;
	public	Transform		lunchPoint;

	// Use this for initialization
	void Start () {
		my_Nma = GetComponent<NavMeshAgent> ();
		my_Anim = GetComponent<Animator> ();
		my_Rbdy = GetComponent<Rigidbody> ();
		my_col = GetComponent<CapsuleCollider> ();
		Player = GameObject.FindGameObjectWithTag ("Player").gameObject;
		MSpawner = GameObject.FindGameObjectWithTag ("SpawnerManager").gameObject.GetComponent<SpawnerManager>();
		Speed = Mathf.RoundToInt (4 * factor);
		my_Nma.speed = Speed;
		if (type == Type.Boss) {
			Damage = Mathf.RoundToInt (20 * factor);
			HP = Mathf.RoundToInt(factor * 400);
			Target = Player;
			DetectZone = 9000;
			nextHadoken = 10;
			lunchPoint = transform.GetChild (0);
		} else {
			Speed = Mathf.RoundToInt (4 * (factor/2));
			if (Speed > 8)
				Speed = 8;
			Damage = Mathf.RoundToInt (5 * factor);
			HP = Mathf.RoundToInt(factor * 100);
			DetectZone = 25;
		}
	}
	
	IEnumerator DeathAnimAndDestroy(){
		yield return new WaitForSeconds (my_Anim.GetCurrentAnimatorClipInfo (0).Length + 2f);
		transform.Translate(new Vector3(0, -1, 0) * Time.deltaTime);
		yield return new WaitForSeconds (2f);
		if (type == Type.Boss)
			MSpawner.IsBoss = false;
		MSpawner.NB -= 1;
		Destroy (this.gameObject);
	}

	public void GetHit(int dam){
		if (HP > 0){
			HP -= dam;
			if (HP <= 0){
				HP = 0;
				isDead = true;
				my_Rbdy.useGravity = false;
				my_Nma.enabled = false;
				my_col.enabled = false;
			} else
				TargetPos = Player.transform.position;
		}
	}

	void OnTriggerEnter(Collider col){
		if (col.gameObject.tag == "Player") {
			my_Nma.updatePosition = false;
			my_Anim.SetBool ("Run", false);
			IsReach = true;
			my_Anim.SetBool ("Attack", true);
		}
	}

	void OnTriggerStay(Collider col){
		if (col.gameObject.tag == "Player") {
			IsAttackable = true;
			if (ftime > 0.1f) {
				StartCoroutine (Attack ());
				ftime = -1.45f;
			}
			ftime += Time.deltaTime;
		}
	}

	void OnTriggerExit(Collider col){
		if (col.gameObject.tag == "Player") {
			ftime = 0;
			if (!isDead){
				IsAttackable = false;
				my_Nma.updatePosition = true;
				my_Nma.SetDestination (col.gameObject.transform.position);
				if (my_Anim.GetBool ("Attack"))
					my_Anim.SetBool ("Attack", false);
				my_Anim.SetBool ("Run", true);
				IsReach = false;
			}
		}
	}

	void Hadoken(){
		if (fhadokentime > nextHadoken) {
//			Vector3 tmp = new Vector3(transform.position.x, transform.position.y + 5f, transform.position.z);
//				new Vector3 (Player.transform.position.x, Player.transform.position.y + 1f,Player.transform.position.z);
			fhadokentime = 0;
			nextHadoken = Random.Range(3,5);
			Instantiate (hadoken, lunchPoint.position, Quaternion.identity);
		}
		fhadokentime += Time.deltaTime;
	}

	public IEnumerator Attack(){
		yield return new WaitForSeconds (0.5f);
		if (IsAttackable)
			Player.GetComponent<PlayerScript>().getHit (Damage);
	}

	// Update is called once per frame
	void Update () {
		if (isDead) {
			if (!my_Anim.GetBool ("Death"))
				my_Anim.SetBool ("Death", true);
			StartCoroutine (DeathAnimAndDestroy ());
			return;
		}
		if (!Target && !IsReach) {
			if (!my_Anim.GetBool ("Run"))
				my_Anim.SetBool ("Run", true);
			my_Nma.SetDestination (TargetPos);
		} else if (Target) {
			my_Nma.SetDestination (Target.transform.position);
			if (!my_Anim.GetBool ("Run") && !my_Anim.GetBool ("Attack"))
				my_Anim.SetBool ("Run", true);
		}
		if (type == Type.Zombitch) {
			if (Vector3.Distance (transform.position, Player.transform.position) < DetectZone) {
				Target = Player;
			} else if (Target) {
				TargetPos = Target.transform.position;
				Target = null;
			}
		} else
			Hadoken ();
	}
}

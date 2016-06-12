using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {
	
	public GuiEnemyHealthBar	guiHealthBar;
	public StatScript 		Sstat;
	public int 				DetectZone;
	public GameObject 		player;
	private NavMeshAgent 	current_nma;
	private Rigidbody 		current_rb;
	private Animator		current_anim;
	private	CapsuleCollider	current_col;
	public bool				isDead;
	public 	EnemySpawners	Sspawner;
	public 	GameObject		gSpawner;
	private	StatScript		PlayerStat;
	private	bool			isAttack;

	// Use this for initialization
	void Start () {
		Sstat = GetComponent<StatScript> ();
		current_nma = GetComponent<NavMeshAgent> ();
		current_col = GetComponent<CapsuleCollider> ();
		current_anim = GetComponent<Animator> ();
		current_rb = GetComponent<Rigidbody> ();
		Sspawner = gSpawner.GetComponent<EnemySpawners> ();
		player = GameObject.FindGameObjectWithTag ("Player");
		PlayerStat = player.GetComponent<StatScript> ();
		DetectZone = 10;
	}

	void OnMouseOver(){
		if (!PlayerStat.dead && guiHealthBar && !guiHealthBar.isActive)
			guiHealthBar.ChangeGuiTarget (this.gameObject, false);
	}
	
	void OnMouseExit(){
		if (!PlayerStat.dead && guiHealthBar && guiHealthBar.isActive && !guiHealthBar.isLock)
			guiHealthBar.DisableGui ();
	}
	
	void OnTriggerStay(Collider col){
		if (col.gameObject.tag == "Player" && current_anim.GetBool ("Attack")) {
			if (!isAttack){
				StartCoroutine (Attack());
			}
		}
	}

	IEnumerator Attack(){
		isAttack = true;
		yield return new WaitForSeconds (1f);
		int tmp = Mathf.RoundToInt(Sstat.ft_ChanceOfHit (PlayerStat));
		if (tmp <= Random.Range (0, 101))
			PlayerStat.HP -= Sstat.ft_FinalDamage (PlayerStat);
		isAttack = false;
	}

	IEnumerator DeathAnimAndDestroy(){
		yield return new WaitForSeconds (current_anim.GetCurrentAnimatorClipInfo (0).Length + 2f);
		transform.Translate(new Vector3(0, -1, 0) * Time.deltaTime);
		yield return new WaitForSeconds (2f);
		Destroy (this.gameObject);
	}

	// Update is called once per frame
	void Update () {
		if (isDead) {
			current_anim.SetBool ("Death", true);
			StartCoroutine(DeathAnimAndDestroy());
			return;
		}
		if (Vector3.Distance (transform.position, player.transform.position) < DetectZone) {
			if (PlayerStat.dead){
				current_anim.SetBool("Run", false);
				current_anim.SetBool("Attack", false);
				current_nma.SetDestination (transform.position);
				return ;
			}
			DetectZone = 500;
			current_nma.SetDestination (player.transform.position);
			current_anim.SetBool ("Run", true);
			transform.LookAt(player.transform);
			if (Vector3.Distance (transform.position, player.transform.position) < current_nma.stoppingDistance) {
				current_anim.SetBool ("Run", false);
				current_anim.SetBool ("Attack", true);
				current_nma.updatePosition = false;
				current_nma.updateRotation = false;
			}
			if (Vector3.Distance (transform.position, player.transform.position) > current_nma.stoppingDistance) {
				current_anim.SetBool ("Attack", false);
				current_anim.SetBool ("Run", true);
				current_nma.updatePosition = true;
				current_nma.updateRotation = false;
			}
		}
		if (Sstat.HP <= 0 && !isDead) {
			isDead = true;
			current_anim.SetBool ("Attack", false);
			current_col.enabled = false;
			current_nma.enabled = false;
			current_col.enabled = false;
			current_rb.useGravity = false;
			Sspawner.isDead = true;
		}

	}
}

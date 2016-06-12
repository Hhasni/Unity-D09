using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class CharacterScript : MonoBehaviour {
	
	private StatScript 			Sstat;
	private NavMeshAgent 		player1_nma;
	private Animator 			player1_anim;
	public 	GameObject			gTarget;
	private	EnemyScript			gTargetScript;
	private	bool				isReach;
	private int					old_random;
	public GuiEnemyHealthBar	guiHealthBar;
	private Image				guiImage;
	private float				fTimer;
	
	RaycastHit hit;
	// Use this for initialization
	void Awake () {
		Sstat = GetComponent<StatScript> ();
		player1_nma = GetComponent<NavMeshAgent> ();
		player1_anim = GetComponent<Animator> ();
		guiImage = GameObject.FindGameObjectWithTag ("Gui").GetComponent<Image> ();
		
	}

	void OnTriggerExit(Collider col){
		if (col.gameObject.tag == "enemy" && gTarget && gTarget.gameObject.GetInstanceID () == col.gameObject.GetInstanceID ())
			gTarget = null;
	}

	
	void OnTriggerStay(Collider col){
		if (col.gameObject.tag == "enemy" && gTarget && gTarget.gameObject.GetInstanceID () == col.gameObject.GetInstanceID ()) {
			if (gTargetScript.Sstat.HP <= 0){
				Sstat.XP += gTargetScript.Sstat.XP;
				Sstat.Money += gTargetScript.Sstat.Money;
				player1_anim.SetBool ("Attack", false);
				gTarget = null;
				guiHealthBar.DisableGui();
				gTargetScript = null;
			}
			else if (!player1_anim.GetBool ("Attack"))
				player1_anim.SetBool ("Attack", true);
		} else if (!gTarget && col.gameObject.tag == "enemy" && isReach) {
			gTarget = col.gameObject;
			guiHealthBar.ChangeGuiTarget(gTarget, true);
			gTargetScript = gTarget.GetComponent<EnemyScript> ();
			player1_anim.SetBool ("Attack", true);
		}
	}

	public void	Attack(){
		int tmp = Mathf.RoundToInt(Sstat.ft_ChanceOfHit (gTargetScript.Sstat));
		int rdm = Random.Range (0, 101);
		while (rdm == old_random){
			rdm = Random.Range (0, 101);
		}
		if (old_random < tmp)
			rdm += 30;
		if (tmp <= rdm){
			gTargetScript.Sstat.HP -= Sstat.ft_FinalDamage (gTargetScript.Sstat);
			old_random = rdm;
		}
		else{
			old_random = rdm;
	//		Debug.Log ("rdm = " + rdm + " " + "old_random = " + old_random + " " + gameObject.tag + " " + gameObject.name + " miss " + gTarget.tag + " " + gTarget.name);
		}
	}

	void OnTriggerEnter(Collider col){
		if (col.gameObject.tag == "enemy" && gTarget && gTarget.gameObject.GetInstanceID() == col.gameObject.GetInstanceID()) {
			player1_nma.SetDestination (transform.position);
			isReach = true;
			player1_anim.SetBool("Attack", true);
		}
	}

	void DeathOfMaya(){
		if (!player1_anim.GetBool("Death")) {
			player1_anim.SetBool ("Attack", false);
			player1_anim.SetBool ("Run", false);
			gTarget = null;
			gTargetScript = null;
			player1_anim.SetBool ("Death", true);
		}
		float alpha = Mathf.Lerp(0f, 1f, fTimer/10f);
		guiImage.color = new Color (0, 0, 0, alpha);
		fTimer += Time.deltaTime;
	}
	
	// Update is called once per frame
	void Update () {
		if (Sstat.dead) {
			DeathOfMaya();
			return;
		}
		if (Input.GetMouseButtonDown (0) || (!gTarget && Input.GetMouseButton (0))) {
			player1_nma.updatePosition = true;
			Ray screenRay = Camera.main.ScreenPointToRay (Input.mousePosition);
			if (Physics.Raycast (screenRay, out hit)) {
				if (hit.collider.gameObject.tag == "enemy"){
					gTarget = hit.collider.gameObject;
					gTargetScript = gTarget.GetComponent<EnemyScript>();
					guiHealthBar.ChangeGuiTarget(gTarget, true);
					isReach = false;
					player1_nma.SetDestination (hit.collider.transform.position);
					player1_anim.SetBool("Run", true);
				}
				else{
					//Debug.Log("ELSE");
					gTarget = null;
					guiHealthBar.DisableGui();
					player1_anim.SetBool("Attack", false);
					player1_nma.SetDestination (hit.point);
					player1_anim.SetBool("Run", true);
					isReach = false;
				}
			}
		}
		if (Vector3.Distance (transform.position, player1_nma.destination) <= player1_nma.stoppingDistance) {
			player1_anim.SetBool ("Run", false);
			player1_nma.updatePosition = false;
		}
		if (gTarget){
			transform.LookAt(gTarget.transform.position);
			if (!isReach)
				player1_nma.SetDestination (gTarget.transform.position);
		}
		if (!gTarget && player1_anim.GetBool("Attack"))
			player1_anim.SetBool ("Attack", false);
	}
}

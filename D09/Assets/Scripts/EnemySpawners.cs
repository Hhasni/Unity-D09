using UnityEngine;
using System.Collections;

public class EnemySpawners : MonoBehaviour {
	
	public float 			offset;
	public float 			TimeRespw;
	private float 			timer;
	public GameObject[] 	list;
	public bool				isDead;

	void Awake () {
		offset = 10;
	}

	public void respawn(){
		GameObject tmp = Instantiate (list [Random.Range (0, 2)], new Vector3 (Random.Range (transform.position.x - offset, transform.position.x + offset),
			                                     22,
			                                     Random.Range (transform.position.z - offset, transform.position.z + offset)),
			             						 Quaternion.identity) as GameObject;
		tmp.GetComponent<EnemyScript> ().gSpawner = this.gameObject;
	}

	// Update is called once per frame
	void Update () {
		if (isDead){
			timer += Time.deltaTime;
			if (timer > TimeRespw){
				timer = 0;
				respawn();
				isDead = false;
			}
		}
	}
}

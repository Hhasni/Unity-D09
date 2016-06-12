using UnityEngine;
using System.Collections;

public class SpawnerScript : MonoBehaviour {
	
	public GameObject gEnemy;
	public GameObject gBoss;

	// Use this for initialization
	void Start () {
	
	}

	public void SpawnBossEnemy(int V){
		GameObject tmp = Instantiate (gBoss, transform.position, Quaternion.identity) as GameObject;
		tmp.GetComponent<ZombitchScript> ().factor = 1 + (V / 4);
	}

	public void SpawnEnemy(int V){
		GameObject tmp = Instantiate (gEnemy, transform.position, Quaternion.identity) as GameObject;
		tmp.GetComponent<ZombitchScript> ().factor = 1f + (V / 4f);
	}

	// Update is called once per frame
	void Update () {
	
	}
}

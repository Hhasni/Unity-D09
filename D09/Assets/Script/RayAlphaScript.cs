using UnityEngine;
using System.Collections;

public class RayAlphaScript : MonoBehaviour {


	// Use this for initialization
	void Start () {
	
	}

	void OnTriggerStay(Collider col){
		if (col.gameObject.tag == "enemy") {
			Debug.Log("HITTING");
			col.gameObject.transform.root.gameObject.GetComponent<ZombitchScript> ().GetHit (2);
		}
	}

	// Update is called once per frame
	void Update () {
	
	
	}
}

using UnityEngine;
using System.Collections;

public class SmokeScript : MonoBehaviour {

	private ParticleSystem _ps;
	private float _lifeTime;
	private float _time;
	// Use this for initialization
	void Start () {
		_ps = GetComponent<ParticleSystem> ();
		_lifeTime = _ps.startLifetime;
		_time = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (gameObject.name != "SmokeAlpha" && gameObject.name != "RayAlpha") {
			_time += Time.deltaTime;
			if (_time > _lifeTime)
				GameObject.Destroy (gameObject);
		}
	}
}

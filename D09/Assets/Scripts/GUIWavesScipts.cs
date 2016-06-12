using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUIWavesScipts : MonoBehaviour {


	public 	SpawnerManager	WaveManager;
	private	Text			tTimer;
	private	Text			tText;
	private	PlayerScript	sPlayer;
	private float			FadeTime;
	public 	float			FadeEnd;
	private float			ftimer;
	private	Image			guiImage;
	private	bool			black;
	// Use this for initialization
	void Start () {
		sPlayer = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerScript> ();
		Text[] tmp = GetComponentsInChildren<Text> ();
		tText = tmp [0];
		tTimer = tmp [1];
		guiImage = GetComponent<Image> ();
		FadeTime = 6f;
		FadeEnd = 4f;
	}

	public void restart(int i){
		tText.text = "Wave " + (WaveManager.NbWaves - i).ToString();
		ftimer = 0;
	}

	IEnumerator restart(){
		yield return new WaitForSeconds (10f);
		Application.LoadLevel (Application.loadedLevel);
	}

	void		death(){
		float alpha = Mathf.Lerp(0f, 1f, ftimer/10f);
		guiImage.color = new Color (0, 0, 0, alpha);
		float alpha2 = Mathf.Lerp (0f, 1f, ftimer / FadeEnd);
		tText.color = new Color (tText.color.r, tText.color.g, tText.color.b, alpha2);	
		ftimer += Time.deltaTime;
		StartCoroutine (restart ());

	}

	// Update is called once per frame
	void Update () {
		if (sPlayer.isDead) {
			if (!black){
				restart(1);
				black = true;
			}
			death();
			return ;
		}
		if (WaveManager.IsBoss)
			tTimer.enabled = false;
		else
			tTimer.enabled = true;
		if (!WaveManager.isPaused) {
			if (ftimer < FadeEnd) {
				float alpha = Mathf.Lerp (0f, 1f, ftimer / FadeEnd);
				tText.color = new Color (tText.color.r, tText.color.g, tText.color.b, alpha);	
			} else {
				float alpha = Mathf.Lerp (1f, 0f, ftimer / FadeTime);
				tText.color = new Color (tText.color.r, tText.color.g, tText.color.b, alpha);	
			}
			tTimer.text = "Time: " + (Mathf.RoundToInt (60 - WaveManager.fWaveTimer)).ToString () + "s";
		} else
			tTimer.text = "Pause: " + (Mathf.RoundToInt (25 - WaveManager.fPausedTimer)).ToString () + "s";
		if (ftimer < FadeTime)
			ftimer += Time.deltaTime;
	}

}

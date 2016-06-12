using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUILife : MonoBehaviour {

	
	private Text			tLife;
	private int				bkpLife;
	public	PlayerScript	sPlayer;
	private Scrollbar 		ScrollB;
	// Use this for initialization
	void Start () {
		sPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
		tLife = transform.GetChild (2).GetComponent<Text> ();
		ScrollB = transform.GetChild (1).GetComponent<Scrollbar> ();
		bkpLife = sPlayer.HP;
	}
	
	// Update is called once per frame
	void Update () {
		if (bkpLife != sPlayer.HP) {
			bkpLife = sPlayer.HP;
			if (bkpLife >= 0){
				tLife.text = sPlayer.HP.ToString ();
				ScrollB.size = bkpLife/100f;
			}
		}
	}
}

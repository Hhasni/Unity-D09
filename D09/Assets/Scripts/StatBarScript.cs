using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StatBarScript : MonoBehaviour {
	
	private RectTransform	rtLife;
	public Text			myText;
	public StatScript 		PlayerStat;
	public int		 		bkpHP;
	// Use this for initialization
	void Start () {
		PlayerStat = GameObject.FindGameObjectWithTag ("Player").GetComponent<StatScript> ();
		Image[] tmp = GetComponentsInChildren<Image>();
		rtLife = tmp [1].GetComponent<RectTransform> ();
		bkpHP = 0;	
	}
	
	// Update is called once per frame
	void Update () {
		if (bkpHP != PlayerStat.HP) {
			myText.text = PlayerStat.HP.ToString() + " hp";
			rtLife.localPosition = new Vector3 (-100 + ((100 / PlayerStat.MaxHp) * PlayerStat.HP),
		                                    0,
		                                    0);
			bkpHP = PlayerStat.HP;
			
		}
		if (PlayerStat.dead)
			gameObject.transform.parent.gameObject.SetActive (false);

	}
}

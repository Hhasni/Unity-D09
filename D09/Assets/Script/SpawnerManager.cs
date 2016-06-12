using UnityEngine;
using System.Collections;

public class SpawnerManager : MonoBehaviour {

	private SpawnerScript[]	TabSpawner;
	public	int 			NB;
	public	int 			nextSpawn;
	public	float 			fSpawnTimer;
	public	float 			fWaveTimer;
	public	float 			fPausedTimer;
	public	int				NbWaves;
	public	bool			isPaused;
	public 	bool			IsBoss;
	public int 				maxTimeSpawn;
	public	int				MaxPauseTime;
	public	int				MaxWaveTime;
	public	GUIWavesScipts	sGUIws;
	// Use this for initialization
	void Start () {
		TabSpawner = GetComponentsInChildren<SpawnerScript> ();
		maxTimeSpawn = 10;
		NB = 0;
//		NbWaves = 1;
		MaxPauseTime = 25;
		MaxWaveTime = 60;
		if (IsBoss)
			SpawnBoss();
	}

	void SpawnNow(){
		TabSpawner [Random.Range (1, TabSpawner.Length)].SpawnEnemy (NbWaves);
		NB += 1;
		nextSpawn = Random.Range (1, maxTimeSpawn);
		fSpawnTimer = 0;
	}

	void SpawnBoss(){
		TabSpawner [Random.Range (1, TabSpawner.Length)].SpawnBossEnemy (NbWaves);
		NB += 1;
	}

	void next_Wave(){
		Debug.Log("next");
		fSpawnTimer = 0;
		fPausedTimer = 0;
		NbWaves += 1;
		if (NbWaves % 3 == 0) {
			SpawnBoss();
			IsBoss = true;
		}
		else {
			IsBoss = false;
			fWaveTimer = 0;
			isPaused = false;
		}
		sGUIws.restart (0);
		if (NbWaves <= 5 && NbWaves != 3)
			maxTimeSpawn -= 1;
	}

	void EndBossWave(){
		IsBoss = false;
	}

	// Update is called once per frame
	void Update () {
		if (!IsBoss) {
			if (!isPaused && fSpawnTimer > nextSpawn && NB < 20)
				SpawnNow ();
			if (fWaveTimer > 60) {
				isPaused = true;
				fWaveTimer = 0;
			}
			if (isPaused)
				fPausedTimer += Time.deltaTime;
			else {
				fWaveTimer += Time.deltaTime;
				fSpawnTimer += Time.deltaTime;
			}
			if (isPaused && fPausedTimer > 25)
				next_Wave ();
		}
	}
}

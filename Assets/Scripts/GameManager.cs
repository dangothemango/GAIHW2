using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager INSTANCE;
    public Agent red;
    public Transform redSpawn;
    public Agent hunter;
    public Transform hunterSpawn;
    public Agent wolf;
    public Transform wolfSpawn;
    int storyCounter = 0;
    

	// Use this for initialization
	void Start () {
		if (INSTANCE != null) {
            this.enabled = false;
            return;
        }
        INSTANCE = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void AdvanceStory() {
        switch (storyCounter) {
            case 0:
                SpawnRed();
                storyCounter++;
                break;
            case 1:
                SpawnWolf();
                storyCounter++;
                break;
            case 2:
                StopRedAndWolf();
                storyCounter++;
                break;
            case 3:
                SendWolfToGrandma();
                storyCounter++;
                break;
            case 4:
                SpawnHunterReleaseWolf();
                storyCounter++;
                break;
        }
    }

    void SpawnRed() {
        red.transform.position = redSpawn.position;
        red.SetState(Agent.State.path);
    }

    void SpawnWolf() {

    }

    void StopRedAndWolf() {

    }

    void SendWolfToGrandma() {

    }

    void SpawnHunterReleaseWolf() {

    }
}

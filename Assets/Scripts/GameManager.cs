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
    public Transform grandmasHouse;
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

    public void AdvanceStory() {
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
        hunter.SetState(Agent.State.wait);
        wolf.SetState(Agent.State.wait);
        red.transform.position = redSpawn.position;
        red.SetState(Agent.State.path);
        StartCoroutine(WaitAndAdvanceStory(2f));
    }

    void SpawnWolf() {
        wolf.transform.position = wolfSpawn.position;
        wolf.SetState(Agent.State.pursue);
        wolf.SetTarget(red.transform);
    }

    void StopRedAndWolf() {
        wolf.SetState(Agent.State.wait);
        red.SetState(Agent.State.wait);
    }

    void SendWolfToGrandma() {
        wolf.SetState(Agent.State.pursue);
        wolf.SetTarget(grandmasHouse);
        red.SetState(Agent.State.path);
    }

    void SpawnHunterReleaseWolf() {
        hunter.transform.position = hunterSpawn.position;
        wolf.SetState(Agent.State.wander);
        hunter.SetState(Agent.State.pursue);
        hunter.SetTarget(wolf.transform);
    }

    IEnumerator WaitAndAdvanceStory(float seconds) {
        yield return new WaitForSeconds(seconds);
        AdvanceStory();
    }
}

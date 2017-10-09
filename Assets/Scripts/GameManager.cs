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
    public int storyCounter = 0;
    

	// Use this for initialization
	void Start () {
		if (INSTANCE != null) {
            this.enabled = false;
            return;
        }
        INSTANCE = this;
        AdvanceStory();
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
                ReactivateRed();
                storyCounter++;
                break;
            case 5:
                SpawnHunterReleaseWolf();
                storyCounter++;
                break;
            case 6:
                EndStory();
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
        red.GetComponent<BoxCollider2D>().enabled = false;
        StartCoroutine(WaitAndAdvanceStory(2f));
    }


    void SendWolfToGrandma() {
        wolf.SetState(Agent.State.pursue);
        wolf.SetTarget(grandmasHouse);
        red.SetState(Agent.State.path);
        StartCoroutine(WaitAndAdvanceStory(3f));
    }

    void ReactivateRed() {
        red.GetComponent<BoxCollider2D>().enabled = true;
    }


    void SpawnHunterReleaseWolf() {
        Destroy(red.gameObject);
        hunter.transform.position = hunterSpawn.position;
        wolf.SetState(Agent.State.wander);
        wolf.SetTarget(hunter.transform);
        hunter.SetState(Agent.State.pursue);
        hunter.SetTarget(wolf.transform);
    }

    void EndStory() {
        wolf.SetState(Agent.State.wait);
        hunter.SetState(Agent.State.wait);
        hunter.enabled = false;
        wolf.enabled = false;
    }

    IEnumerator WaitAndAdvanceStory(float seconds) {
        yield return new WaitForSeconds(seconds);
        AdvanceStory();
    }
}

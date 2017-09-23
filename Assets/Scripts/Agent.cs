using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour {

    public enum State {
        wait,
        wander,
        pursue,
        evade,
        path
    }

    public State curState = State.wait;
    public GameObject target;
    public Transform[] path;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		switch (curState) {
            case State.wait:
                break;
            case State.wander:
                Wander();
                break;
            case State.pursue:
                Pursue();
                break;
            case State.evade:
                Evade();
                break;
            case State.path:
                FollowPath();
                break;
        }
	}

    void Wander() {

    }

    void Pursue() {

    }

    void Evade() {

    }

    void FollowPath() {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf : MonoBehaviour {

    Agent a;

	// Use this for initialization
	void Start () {
        a = GetComponent<Agent>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.name == "Hunter") {
            a.SetState(Agent.State.evade);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.name == "Hunter") {
            a.SetState(Agent.State.wander);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        GameManager.INSTANCE.AdvanceStory();
    }
}

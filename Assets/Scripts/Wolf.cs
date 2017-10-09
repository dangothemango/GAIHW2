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
            a.SetTarget(other.transform);
        } else {
            a.SetState(Agent.State.pursue);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.name == "Hunter") {
            StartCoroutine(WaitAndWander());
        } else {
            StartCoroutine(WaitAndWander());
        }
    }

    IEnumerator WaitAndWander() {
        yield return new WaitForSeconds(1f);
        a.SetState(Agent.State.wander);
        a.newDir();
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        Debug.Log("Collide");
        GameManager.INSTANCE.AdvanceStory();
    }
}

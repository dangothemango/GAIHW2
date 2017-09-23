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
    public Transform target;
    public Transform[] path;
    public float rotation_speed;
    public float move_speed;
    public int path_index;
    public GameObject wander_target;

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
            default:
                Debug.LogError(string.Format("{0} is not a valid state",curState));
                SetState(State.wait);
                break;
        }
	}

    void SetState(State s) {
        Debug.Log(s);
        curState = s;
    }

    void Wander() {
        if (wander_target == null)
        {
            wander_target = new GameObject("Wanderer");
            wander_target.transform.position = new Vector2(Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f));
        }

        float distance = Vector2.Distance(wander_target.transform.position, transform.position);
        if (distance < 1.0f)
        {
            wander_target.transform.position = new Vector2(Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f));
        }

        Vector3 displacement = wander_target.transform.position - transform.position;
        displacement = displacement.normalized;
        
        transform.position += displacement * move_speed * Time.deltaTime;
    }

    void Pursue() {
        Vector3 displacement = target.position - transform.position;
        displacement = displacement.normalized;
        
        if (Vector2.Distance(target.position, transform.position) > 1.0f) {
            transform.position += displacement * move_speed * Time.deltaTime;
        }
    }

    void Evade() {

    }

    void FollowPath() {
        if (path_index < path.Length - 1)
        {
            //Check if within range of path point to move to next point
            float distance = Vector2.Distance(path[path_index + 1].position, transform.position);
            if (distance < 1.0f)
            {
                ++path_index;
            }
            
            Vector3 displacement = path[path_index + 1].position - transform.position;
            displacement = displacement.normalized;

            transform.position += displacement * move_speed * Time.deltaTime;
        }
    }
}

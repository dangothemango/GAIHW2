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

    public void SetState(State s) {
        Debug.Log(s);
        curState = s;
    }

    void Wander() {

    }

    void Pursue() {
        transform.rotation = normalize(Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target.position - transform.position), rotation_speed * Time.deltaTime));
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

        //Update the rotation and position of the agent according to the next point in the path
            transform.rotation = normalize(Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(path[path_index+1].position - transform.position), rotation_speed * Time.deltaTime));
            transform.position += displacement * move_speed * Time.deltaTime;
        }
    }

    Quaternion normalize(Quaternion q) {
        return Quaternion.Euler(0, 0, q.eulerAngles.z);
    }
}

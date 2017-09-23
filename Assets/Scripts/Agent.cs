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
    public float slow_down_dist;
    public int path_index;
    public float speed;
    public Transform evade_target;
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

    public void SetState(State s) {
        Debug.Log(s);
        curState = s;
    }

    public void SetTarget(Transform t) {
        target = t;
    }

    void Wander() {
        if (wander_target == null) {
            wander_target = new GameObject("Wanderer");
            wander_target.transform.position = new Vector2(Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f));
        }

        float distance = Vector2.Distance(wander_target.transform.position, transform.position);
        if (distance < 1.0f) {
            wander_target.transform.position = new Vector2(Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f));
        }

        DynamicArrival(distance);
        RotateTowards(wander_target.transform);
        MoveTo(wander_target.transform);
    }

    void Pursue() {
        float distance = Vector2.Distance(target.position, transform.position);
        DynamicArrival(distance);

        if (distance > 1.0f)
        {
            RotateTowards(target);
            MoveTo(target);
        }
    }

    void Evade()
    {
        evade_target.position = target.InverseTransformPoint(transform.position);
        RotateTowards(evade_target);
        MoveTo(evade_target);
    }

    void FollowPath() {
        if (path_index < path.Length - 1)
        {
            //Check if within range of path point to move to next point
            float distance = Vector2.Distance(path[path_index + 1].position, transform.position);
            if (distance < 0.1f) {
                ++path_index;
            }

            DynamicArrival(distance);
            RotateTowards(path[path_index + 1]);
            MoveTo(path[path_index + 1]);
        }
    }

    void DynamicArrival(float distance)
    {
        if (distance < slow_down_dist)
        {
            speed = move_speed * distance / slow_down_dist;
        }
        else
        {
            speed = move_speed;
        }
    }

    void RotateTowards(Transform t)
    {
        transform.rotation = normalize(Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(t.position - transform.position), rotation_speed * Time.deltaTime));
    }

    void MoveTo(Transform t)
    {
        Vector3 displacement = t.position - transform.position;
        displacement = displacement.normalized;

        transform.position += displacement * speed * Time.deltaTime;
    }

    Quaternion normalize(Quaternion q) {
        return Quaternion.Euler(0, 0, q.eulerAngles.z);
    }
}

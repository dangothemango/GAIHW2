﻿using System.Collections;
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
    public Transform wander_target;
    public int path_index;
    Vector3 dir = Vector3.zero;

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
        if (dir==Vector3.zero || Random.Range(0, 1.0f) > .99f) {
            newDir();
        }
        Debug.DrawLine(transform.position, transform.position + dir,Color.black);
        transform.position = Vector2.MoveTowards(transform.position, transform.position+dir, Time.deltaTime * move_speed/2);
        RotateTowards(transform.position+dir);
    }

    public void newDir() {
        dir = (Vector2)wander_target.position + Random.insideUnitCircle - (Vector2)transform.position;
    }

    void Pursue() {
        float distance = Vector2.Distance(target.position, transform.position);


        if (distance > .5f)
        {
            RotateTowards(target.position);
            transform.position = Vector2.MoveTowards(transform.position, target.position,Time.deltaTime* move_speed * Mathf.Min(distance / slow_down_dist, 1));
        }
    }

    void Evade()
    {
        Vector3 v = transform.position - target.position;
        RotateTowards(transform.position+v);
        transform.position = Vector2.MoveTowards(transform.position, transform.position+v, Time.deltaTime * move_speed);
    }

    void FollowPath() {
        if (path_index < path.Length - 1)
        {
            //Check if within range of path point to move to next point
            float distance = Vector2.Distance(path[path_index + 1].position, transform.position);
            if (distance < 0.1f) {
                ++path_index;
            }

            distance = Vector2.Distance(target.position, transform.position);

            transform.position = Vector2.MoveTowards(transform.position, path[path_index+1  ].position, Time.deltaTime * move_speed * Mathf.Min(distance / slow_down_dist, 1));
            RotateTowards(path[path_index + 1].position);
        }
    }

    void RotateTowards(Vector3 position)
    {
        Vector3 offset = (position - transform.position).normalized;
        transform.right = Vector3.MoveTowards(transform.right, offset, Time.deltaTime * rotation_speed);
    }
    
}

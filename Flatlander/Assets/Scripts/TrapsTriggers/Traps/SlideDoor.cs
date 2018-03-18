using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideDoor : Trap {

    private Vector3 inactivePos;
    private Vector3 activePos;

    public float unitsToMove;
    public float moveSpeed;

    public bool openOnRelic;

	// Use this for initialization
	void Start () {
        active = false;

        gm = FindObjectOfType<GM>();
        
        // The door's inactive (default) position will be where it's placed in the world initially.
        // When deactivated, it will move towards this initial position.
        inactivePos = transform.position;
        activePos.Set(transform.position.x, transform.position.y + unitsToMove, transform.position.z);
    }

    public override void checkActive()
    {
        if (openOnRelic && gm.phase == GM.Phases.ESCAPE)
        {
            active = true;
        }

        // If the door is active, not moving, and not at it's target position, moves it towards
        // target position. Does the same for inactivity.
        if (active && !(transform.position == activePos))
        {
            float speed = moveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, activePos, speed);
        }
        else if (!active && !(transform.position == inactivePos))
        {
            float speed = moveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, inactivePos, speed);
        }
    }
}

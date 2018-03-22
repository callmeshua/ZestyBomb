using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenSpikes : Trap {

    public float activationDelay;
    public float activeTime;
    public float moveSpeed;
    public Transform spikeGroup;

    private Vector3 inactivePos;
    private Vector3 activePos;
    private float delayTimer;
    private float activeTimer;

    // Use this for initialization
    void Start () {
        deactivate();
        delayTimer = activationDelay;
        activeTimer = activeTime;

        // The spikes' inactive (default) position will be where it's placed in the world initially.
        // When deactivated, it will move towards this initial position. When activated, they will rise enough
        // to for the player to be able to collide with them.
        inactivePos = spikeGroup.position;
        activePos.Set(spikeGroup.position.x, spikeGroup.position.y + 1f, spikeGroup.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            activate();
        }
    }

    public override void checkActive()
    {
        if (active)
        {
            if (delayTimer <= 0f)
            {
                if (!(spikeGroup.position == activePos))
                {
                    float speed = moveSpeed * Time.deltaTime;
                    spikeGroup.position = Vector3.MoveTowards(spikeGroup.position, activePos, speed);
                }
                else
                {
                    // If activated, tick down the activeTimer until it reaches 0, then deactivate the spikes
                    if (activeTimer <= 0f || gm.resetLevel)
                    {
                        deactivate();
                        activeTimer = activeTime;
                    }
                    else
                    {
                        activeTimer = activeTimer - Time.deltaTime;
                    }
                }
            }
            else
            {
                delayTimer = delayTimer - Time.deltaTime;
            }
        }
        else
        {
            if (!(spikeGroup.position == inactivePos))
            {
                float speed = moveSpeed * Time.deltaTime;
                spikeGroup.position = Vector3.MoveTowards(spikeGroup.position, inactivePos, speed);
            }
            else
            {
                delayTimer = activationDelay;
            }
        }
    }
}

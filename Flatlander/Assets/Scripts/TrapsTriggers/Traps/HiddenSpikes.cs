using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenSpikes : MonoBehaviour {

    public float activationDelay;
    public float moveSpeed;
	private bool active;
	private GM gm;
    private Transform spikeGroup;
    private Vector3 inactivePos;
    private Vector3 activePos;
    private float delayTimer;

    // Use this for initialization
    void Start () {
		gm = FindObjectOfType<GM>();
        active = false;
        delayTimer = activationDelay;
        spikeGroup = transform.GetChild(0);

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
            active = true;
        }
    }

	void Update()
	{
		if (gm.resetLevel) 
		{
			active = false;
		}
		checkActive ();
	}

    public void checkActive()
    {
		if (active&&!gm.resetLevel) {
			if (delayTimer <= 0f) {
				if (!(spikeGroup.position == activePos)) {
					float speed = moveSpeed * Time.deltaTime;
					spikeGroup.position = Vector3.MoveTowards (spikeGroup.position, activePos, speed);
				}
			} else {
				delayTimer = delayTimer - Time.deltaTime;
			}
		} else {
			active = false;
			spikeGroup.position = inactivePos;
			delayTimer = activationDelay;
		}

    }

	public void ResetSpikes()
	{
		spikeGroup.position = inactivePos;
		delayTimer = activationDelay;
	}
}

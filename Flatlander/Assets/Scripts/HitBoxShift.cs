using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * USED
 * FELIPE LAI
*/

public class HitBoxShift : MonoBehaviour {

    //the player
    public SideScrollController pCtrl;
    BoxCollider boxCollider;

    // Use this for initialization
    void Start () {
        boxCollider = this.gameObject.GetComponent<BoxCollider>();
    }
	
	// Update is called once per frame
	void Update () {

        //changes the player's hitbox depending on persp.
        if (pCtrl.isSideScrolling)
        {
            //extends the player's hitbox z value for sidescrolling orthographic trickery
            boxCollider.size = new Vector3(1, 1, 10);
        }
        else if (!pCtrl.isSideScrolling)
        {
            //returns player hitbox to normal for topdown
            boxCollider.size = new Vector3(1, 1, 1);
        }
    }
}

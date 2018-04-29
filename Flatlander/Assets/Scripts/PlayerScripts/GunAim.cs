using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunAim : MonoBehaviour
{
    private GameObject rightShoulderPoint;
    private SideScrollController pCtrl;
    public Vector2 minMaxAim;
    private Transform rightShoulder;
    public GM gm;

    //public GameObject AimReticle;
    // Use this for initialization
    void Start()
    {
        pCtrl = FindObjectOfType<SideScrollController>();
        rightShoulder = pCtrl.rightShoulder;
        gm = FindObjectOfType<GM>();
        rightShoulderPoint = new GameObject();
        rightShoulderPoint.name = transform.root.name + "Right Shoulder IK Helper";
        //AimReticle = GameObject.Find("Reticle");
    }

    // Update is called once per frame
    void Update()
    {
		if (!gm.frozen || !gm.paused || !gm.gameOver)
        {
                HandleShoulder();
        }
    }

    void HandleShoulder()
    {
        Vector3 rightShoulderPos = rightShoulder.TransformPoint(Vector3.zero);
        rightShoulderPoint.transform.position = rightShoulderPos;
        rightShoulderPoint.transform.parent = transform.parent;

        ///procedural animation for aiming
        transform.LookAt(pCtrl.lookPos);

        if (pCtrl.isAnchored)
        {
            transform.LookAt(pCtrl.grapple.anchors[pCtrl.grapple.anchors.Count - 1]);
        }
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x,0f,0f);
        transform.position = rightShoulderPoint.transform.position;

        //RaycastHit aimHit;
        //if (Physics.Raycast(pCtrl.grapple.barrel.transform.position, pCtrl.lookPos-pCtrl.grapple.barrel.transform.position, out aimHit, 1000f, pCtrl.grapple.ropeCollisionMask) && gm.isUsingController && !pCtrl.isAnchored) 
        //{
        //    AimReticle.SetActive(true);
        //    AimReticle.transform.position = aimHit.point;
        //}
        //else
        //{
        //    AimReticle.SetActive(false);
        //}
    }
}

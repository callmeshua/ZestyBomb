﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;
/*
 * USED
 * BEN SPURR
 * 
 * looks at a target and offsets from that target based on distance, position, and angle
*/

public class CameraController : MonoBehaviour
{
    //PUBLICS
    
    [Header ("Target")]
    public Transform target;

    [Header ("Modifiers")]
    public float maxDistance;                                          //max z distance
    public float minDistance;                                          //min z distance
    public Vector3 lookOffset;                                  //the direction the camera is looking relative to the target
    public float movementDamp = 7f;                             //speed of movement
    public float zoomDamp = .5f;                                //speed of zoom

    public bool isZoomedOut;                                    //for cinematic effects
    public float zoomOutDist;
    public bool startZoomed;                                    //start camera on player
    public float orthoSize;
    public float initOrthosize;

    //PRIVATES
    private float zTarget;                  //target z position for dynamic dolly
    private Vector2 curZMinMax;             //vector of zMax and zMins
    private SideScrollController pCtrl;     //gets reference to player controller
    private PostProcessingProfile postProfile;
    private float targetOtho;
    private Vector3 positionOffset = new Vector3(0f, 0f, -1f);   //position of camera relative to player (should be normalized)
    public Vector3 gameOverOffset;
    public GM gm;

    //initializes values
    void Start()
    {
        pCtrl = FindObjectOfType<SideScrollController>();
        gm = FindObjectOfType<GM>();

        //(good for staging starting shots)
        if (startZoomed)
        {
            transform.position = target.position + positionOffset * maxDistance;
        }
        initOrthosize = Camera.main.orthographicSize;
        postProfile = Camera.main.GetComponent<PostProcessingBehaviour>().profile;
    }

    // Late Uptate called after all for no render artifacts/stuttering
    void LateUpdate()
    {
        //HandlePostDOF();
        //dist multiplier based on player speed

        float targetMultiplier;
        float zoom;

        Vector3 curPositionOffset;
        if (gm.gameOver)
        {
            targetMultiplier = minDistance * .2f;
            zoom = zoomDamp * 2f;
            curPositionOffset = new Vector3(gameOverOffset.x, gameOverOffset.y, gameOverOffset.z * zTarget);
            
        }
        else
        {
            targetMultiplier = Mathf.Clamp((pCtrl.currentVelocity / pCtrl.maxSpeed) * maxDistance, minDistance, maxDistance);
            zoom = zoomDamp;
            curPositionOffset = new Vector3(positionOffset.x, positionOffset.y, positionOffset.z * zTarget);
            positionOffset = positionOffset.normalized;
        }

        zTarget = Mathf.Lerp(zTarget, targetMultiplier, Time.deltaTime * zoom);
        
        //move and look

        positionOffset = positionOffset.normalized; //pos offset must be normalized

        //curPositionOffset = new Vector3(positionOffset.x, positionOffset.y, positionOffset.z * zTarget);

        transform.position = Vector3.Lerp(transform.position, target.position + curPositionOffset, Time.deltaTime * movementDamp);

        targetOtho = Mathf.Lerp(Camera.main.orthographicSize, zTarget, Time.deltaTime * zoomDamp);

        if (targetOtho <= 1) {
            Camera.main.orthographicSize = targetOtho;
        }
        else
        {
            Camera.main.orthographicSize = 20;
        }

        transform.LookAt((target.position + lookOffset));
    }
}

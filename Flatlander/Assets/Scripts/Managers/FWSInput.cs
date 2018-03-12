using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * USED
 * BEN SPURR
 * 
 * Handles input from Input Manager
 * 
 * ATTACHED TO InputManager
*/

public class FWSInput : MonoBehaviour {

    //EXTERNAL
    public GameObject pauseScreen;
    public Texture2D reticle;
    public CursorMode cursorMode;
    public GM gm;
    public GameObject aimGO;
    public GameObject lookGo;


    [HideInInspector]
    public bool paused;

    [Header ("Input Values")]
    public float horizontal;
    public float vertical;
    public float horizontalAim;
    public float verticalAim;
    public KeyCode use = KeyCode.E;

    [Header ("Conditionals")]
    public bool snap = false;
    public bool reset;
    public bool isJumping;
    public bool isUsingController;
    public bool isShooting;
    public Vector3 lookPos;

    //INTERNAL
    //private float lastRotate;       //store rotation for controller aiming
    public SideScrollController pCtrl;
    public GrappleController grappleCtrl;
    private Quaternion aimRotation;
    private float aimAngle;
    private bool canShoot;
    // Use this for initialization
    void Start ()
    {
        aimGO = GameObject.Find("AimObject");
        lookGo = GameObject.Find("LookObject");
        gm = FindObjectOfType<GM>();
        pCtrl = FindObjectOfType<SideScrollController>();
        grappleCtrl=FindObjectOfType<GrappleController>();
        paused = gm.frozen;
        cursorMode = CursorMode.Auto;
        Cursor.SetCursor(reticle, new Vector2(reticle.width/2f,reticle.height/2f),CursorMode.Auto);
        canShoot = true;
    }
	
	// Update is called once per frame
	void Update ()
    {
        paused = gm.frozen;


        //ENABLE WITH CONTROLLER SUPPORT

        if(gm.isUsingController)
        {
            verticalAim = Input.GetAxisRaw("Vertical Aim");
            horizontalAim = Input.GetAxisRaw("Horizontal Aim");

            horizontal = Input.GetAxis("Horizontal Controller");
            vertical = Input.GetAxis("Vertical Controller");

        }
        else
        {
            horizontal = Input.GetAxisRaw("Horizontal");
            vertical = Input.GetAxisRaw("Vertical");
        }

        if (!paused)
        {
            if (!pCtrl.isDead)
            {
                //Cursor.visible = false;
                //Cursor.lockState = CursorLockMode.Confined;
            }

            if (gm.isUsingController)
            {



                if (Input.GetAxis("Controller Trigger") == 0f)
                {
                    canShoot = true;
                }

                if (Input.GetAxis("Controller Trigger") > 0f && canShoot && !isShooting)
                {
                    canShoot = false;
                    isShooting = true;
                }
                else
                {
                    isShooting = false;
                }
            }
            else
            {
                isShooting = Input.GetButtonDown("Fire1");
            }
            
            isJumping = Input.GetButtonDown("Jump");
        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            
            if (Input.GetButtonDown("Jump"))
            {
                gm.ResetScene();
            }
            
        }

        if(Input.GetButtonDown("Reset"))
        {
            //pCtrl.DisableRagdoll();
            reset = true;
        }
        else
        {
            reset = false;
        }

        if (Input.GetButtonDown("Cancel"))
        {
            gm.handlePause();
        }

        if (gm.isUsingController)
        {
            HandleControllerAim();
        }
        else
        {
            HandleMouseAim();
        }
    }

    //handles the aim from the mouse
    void HandleMouseAim()  
    {
        //Shoots ray forward for hit point so transform can rotate towards
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //Vector3 point = (ray.GetPoint(Vector3.Distance(ray.origin, grappleCtrl.barrel.transform.position))- grappleCtrl.barrel.transform.position).normalized*4f;
        Vector3 point = ray.GetPoint(Vector3.Distance(ray.origin, grappleCtrl.barrel.transform.position));



        if (pCtrl.isAnchored && grappleCtrl.curHook != null)
        {
            aimGO.transform.rotation.SetLookRotation(grappleCtrl.curHook.transform.position, Vector3.up);
            lookGo.transform.position = grappleCtrl.curHook.transform.position;
        }
        else
        {
            aimGO.transform.rotation.SetLookRotation(point, Vector3.up);
            lookGo.transform.position = point;
        }

        //lookGo.transform.position = new Vector3(lookGo.transform.position.x, lookGo.transform.position.y, 0f);
        lookPos = lookGo.transform.position;
    }

    //NOT FULLY IMPLEMENTED
    //handles the aim from the controller
    void HandleControllerAim()
    {
        ////get vector between camera and player 
        //Vector3 difference = Camera.main.transform.position - pCtrl.transform.position;

        ////why negative difference? idk
        //float camRotate = Mathf.Atan2(-difference.x, -difference.z);

        //float playerRotate = Mathf.Atan2(horizontalAim, verticalAim);

        ////combining the two radians
        //playerRotate = playerRotate + camRotate;

        //float checkRotation = (Mathf.Abs(Mathf.Atan2(verticalAim , horizontalAim)));

        ////store last rotation of player so it doesn't reset when there is no joystick input
        //if (checkRotation > 0.2f)
        //{
        //    lastRotate = playerRotate;
        //}
        //else if (checkRotation < 0.01f && verticalAim > 0)
        //{
        //    lastRotate = 0f;
        //}
        //else
        //{
        //    playerRotate = lastRotate;
        //}

        ////convert radian to degrees
        //Quaternion eulerRotation = Quaternion.Euler(playerRotate * Mathf.Rad2Deg, 90f, 0f);

        ////plugin degree conversion into transform
        //aimRotation = Quaternion.Slerp(aimRotation, eulerRotation, Time.deltaTime * 10);

        float x = horizontalAim;
        float y = verticalAim;
        if (x != 0.0f || y != 0.0f)
        {
            aimAngle = Mathf.Atan2(y, x) * Mathf.Rad2Deg;
        }

        Quaternion eulerRotation = Quaternion.Euler(aimAngle, 90f, 0f);
        aimRotation = Quaternion.Slerp(aimRotation, eulerRotation, Time.deltaTime * 10);

        if (pCtrl.isAnchored && grappleCtrl.curHook != null)
        {
            aimGO.transform.rotation.SetLookRotation(grappleCtrl.curHook.transform.position, Vector3.up);
            lookGo.transform.position = grappleCtrl.curHook.transform.position;
        }
        else
        {
            aimGO.transform.rotation = aimRotation;

            lookGo.transform.localPosition = new Vector3(0f, 0f, 50f);
            lookPos = lookGo.transform.position;
        }
    }
}

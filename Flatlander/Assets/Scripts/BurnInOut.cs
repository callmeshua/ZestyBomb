using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnInOut : MonoBehaviour {
	Renderer r;

	public int Idle{get{return 0;}}
	public int BurnIn{get{return 1;}}
	public int BurnOut{get{return 2;}}

	[HideInInspector] public int state=0;

	public Color fadeInColor;
	public Texture fadeInTexture;
	public float fadeInSpeed=1;
	public Color fadeOutColor;
	public Texture fadeOutTexture;
	public float fadeOutSpeed=1;

	private float startTime=-1;

	// Use this for initialization
	void Start () {
		//make a copy of material at startup, allowing settings to be per-object
		r=GetComponent<Renderer>();
		r.material=new Material(r.material);
	}

	public void fadeIn(){
		r.material.SetColor("_BlendEdge",fadeInColor);
		r.material.SetTexture("_BlendTex",fadeInTexture);

		state=BurnIn;
		startTime=Time.time;
	}

	public void fadeOut(){
		r.material.SetColor("_BlendEdge",fadeOutColor);
		r.material.SetTexture("_BlendTex",fadeOutTexture);

		state=BurnOut;
		startTime=Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		float t=(Time.time-startTime);

		if(state==BurnIn){
			t*=fadeInSpeed;
			float b=Mathf.Min(t,1f);
			r.material.SetFloat("_BlendVal",1-b);
		}else if(state==BurnOut){
			t*=fadeOutSpeed;
			float b=Mathf.Min(t,1f);
			r.material.SetFloat("_BlendVal",b);
		}

		if(t>1){
			/*production should use this, second bit is for debugging
			state=Idle;
			/*/
			if(state==BurnIn){
				fadeOut();
			}else{
				fadeIn();
			}
			//*/
		}
	}
}

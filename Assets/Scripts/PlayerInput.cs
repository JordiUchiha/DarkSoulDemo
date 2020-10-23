using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {

    [Header("===============Key Setting==========")]
    public string KeyUp;
    public string KeyDown;
    public string KeyLeft;
    public string KeyRight;

    public string KeyA;
    public string KeyB;
    public string KeyC;
    public string KeyD;

    [Header("===============Output Signals==========")]
    public float Dup;
    public float Dright;
    public float Dmag;
    public bool run;
    private float targetDup;
    private float targetDright;

    private float velocityDup;
    private float velocityDright;

    [Header("Others")]
    public bool inputEnabled = true;

    

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //key转为signal
        //signal -1-1之间
        targetDup = Input.GetKey(KeyUp)?1.0f:0 - (Input.GetKey(KeyDown)? 1.0f : 0);
        targetDright = Input.GetKey(KeyRight) ? 1.0f : 0 - (Input.GetKey(KeyLeft) ? 1.0f : 0);

        if(inputEnabled==false)
        {
            targetDright = 0.0f;
            targetDup = 0.0f;
        }

        Dup = Mathf.SmoothDamp(Dup, targetDup, ref velocityDup, 0.1f);
        Dright = Mathf.SmoothDamp(Dright, targetDright, ref velocityDright, 0.1f);

        Dmag = Mathf.Sqrt(Dup * Dup + Dright * Dright);

        run = Input.GetKey(KeyA);
    }
}

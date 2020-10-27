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
    public Vector3 DVec;
    public bool run;
    private float targetDup;
    private float targetDright;

    public bool jump;
    private bool lastJump;

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

        Vector2 tempDAxis = SquareToCircle(new Vector2(Dright, Dup));
         float Dright2 = tempDAxis.x;
         float Dup2 = tempDAxis.y;

        Dmag = Mathf.Sqrt(Dup2 * Dup2 + Dright2 * Dright2);
        DVec = Dright2 * transform.right + Dup2 * transform.forward;
        run = Input.GetKey(KeyA);

        bool tempJump = Input.GetKey(KeyB);
        if(tempJump!=lastJump && tempJump==true)
        {
            jump = true;
        }
        else
        {
            jump = false;
        }
        lastJump = tempJump;
    }

    private Vector2 SquareToCircle(Vector2 input)
    {
        Vector2 v = Vector2.zero;
        v.x = input.x * Mathf.Sqrt(1 - (input.y * input.y) / 2.0f);
        v.y = input.y * Mathf.Sqrt(1 - (input.x * input.x) / 2.0f);
        return v;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorController : MonoBehaviour {

    public GameObject model;
    public float walkSpeed=1.4f;
    private PlayerInput pi;
    [SerializeField]
    private Animator anim;
    private Rigidbody rigid;
    
    private Vector3 movingVec;
    
	// Use this for initialization
	void Start () {
        anim = model.GetComponent<Animator>();
        pi = GetComponent<PlayerInput>();
        rigid = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        float targetRunMuti = (pi.run) ? 2.0f : 1.0f;
        anim.SetFloat("forward", pi.Dmag* Mathf.Lerp(anim.GetFloat("forward"),targetRunMuti,0.5f));
        //角色转向
        if(pi.Dmag>0.1f)
        {
            model.transform.forward = Vector3.Slerp(model.transform.forward, pi.DVec, 0.2f);
        }
        movingVec = pi.Dmag * model.transform.forward*walkSpeed*((pi.run)?2.0f:1.0f);
	}

    private void FixedUpdate()
    {
        //刚体相关在这里设置
        rigid.position += movingVec * Time.fixedDeltaTime;
        //rigid.velocity = new Vector3(movingVec.x, rigid.velocity.y, movingVec.z);
    }
}

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
        anim.SetFloat("forward", pi.Dmag* ((pi.run) ? 2.0f : 1.0f));
        //角色转向
        if(pi.Dmag>0.1f)
        {
            model.transform.forward = pi.Dright * transform.right + pi.Dup * transform.forward;
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

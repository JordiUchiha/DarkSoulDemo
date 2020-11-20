using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorController : MonoBehaviour {

    public GameObject model;
    public float walkSpeed = 1.4f;

    public PhysicMaterial frictionZero;
    public PhysicMaterial frictionOne;
    private PlayerInput pi;
    [SerializeField]
    private Animator anim;
    private Rigidbody rigid;

    private Vector3 planarVec;
    private bool lockPlanar; //控制水平方向是否位移

    private Vector3 jumpThrust; //跳起向上的向量
    public float rollVelocity = 2; //翻滚冲量
    public float velocity = 3;

    private bool canAttack;
    private CapsuleCollider capsule;

    private float lerpTarget;

    // Use this for initialization
    void Start() {
        anim = model.GetComponent<Animator>();
        pi = GetComponent<PlayerInput>();
        rigid = GetComponent<Rigidbody>();
        capsule = GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update() {
        float targetRunMuti = (pi.run) ? 2.0f : 1.0f;
        anim.SetFloat("forward", pi.Dmag * Mathf.Lerp(anim.GetFloat("forward"), targetRunMuti, 0.5f));
        if (rigid.velocity.magnitude > 1.0f)
        {
            anim.SetTrigger("roll");
        }
        if (pi.jump)
        {
            anim.SetTrigger("jump");
            canAttack = false;
        }

        //限制攻击条件 动画状态ground
        if (pi.attack && CheckState("ground") &&anim.GetBool("isGround") && canAttack)
        {
            anim.SetTrigger("attack");
        }

        //角色转向
        if (pi.Dmag > 0.1f)
        {
            model.transform.forward = Vector3.Slerp(model.transform.forward, pi.DVec, 0.2f);
        }
        if (lockPlanar == false)
        {
            planarVec = pi.Dmag * model.transform.forward * walkSpeed * ((pi.run) ? 2.0f : 1.0f);
        }

    }

    private bool CheckState(string stateName,string layerName="Base Layer")
    {
        return anim.GetCurrentAnimatorStateInfo(anim.GetLayerIndex(layerName)).IsName(stateName);
    }

    private void FixedUpdate()
    {
        //刚体相关在这里设置
        rigid.velocity = new Vector3(planarVec.x, rigid.velocity.y, planarVec.z) + jumpThrust;
        jumpThrust = Vector3.zero;

    }

    public void OnEnterJump()
    {
        pi.inputEnabled = false;
        lockPlanar = true;
        jumpThrust = new Vector3(0, velocity, 0);
        print("OnEnterJump");
    }

    public void OnExitJump()
    {

        print("OnExitJump");
    }

    public void IsGround()
    {
        print("IsGround");
        anim.SetBool("isGround", true);
    }

    public void IsNotGround()
    {
        print("IsNotGround");
        anim.SetBool("isGround", false);
    }

    public void OnEnterGround()
    {
        canAttack = true;
        pi.inputEnabled = true;
        lockPlanar = false;
        print("OnEnterGround");
        capsule.material = frictionOne;
    }

    public void OnExitGround()
    {
        capsule.material = frictionZero;
    }

    public void OnEnterFall()
    {
        pi.inputEnabled = false;
        lockPlanar = true;
    }

    public void OnEnterRoll()
    {
        pi.inputEnabled = false;
        lockPlanar = true;
        jumpThrust = new Vector3(0, rollVelocity, 0);
        print("OnEnterRoll");
    }

    public void OnEnterJab()
    {
        pi.inputEnabled = false;
        lockPlanar = true;
        print("OnEnterJab");
    }

    public void OnJabUpdate()
    {
        jumpThrust = model.transform.forward * anim.GetFloat("jabVelocity");
    }

    public void OnEnterAttackA()
    {
        pi.inputEnabled = false;
        lerpTarget = 1.0f;
       
    }

    public void OnUpdateAttackA()
    {
        float curWeight = anim.GetLayerWeight(anim.GetLayerIndex("attack"));
        float animWeight = Mathf.Lerp(curWeight, lerpTarget, 0.1f);
        anim.SetLayerWeight(anim.GetLayerIndex("attack"), animWeight);
    }

    public void OnEnterAttackIdle()
    {
        pi.inputEnabled = true;
        lerpTarget = 0.0f;
    }

    public void OnUpdateAttackAToIdle()
    {
        float curWeight = anim.GetLayerWeight(anim.GetLayerIndex("attack"));
        float animWeight = Mathf.Lerp(curWeight, lerpTarget, 0.1f);
        anim.SetLayerWeight(anim.GetLayerIndex("attack"), animWeight);
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGroundSenser : MonoBehaviour {

    public CapsuleCollider capCol;
    private Vector3 point1;
    private Vector3 point2;
    private float radius;
    public float offset = 0.01f;

    private void Awake()
    {
        radius = capCol.radius-0.05f;

    }

    // Update is called once per frame
    void FixedUpdate () {
        point1 = transform.position + transform.up*(radius-offset);
        point2 = transform.position + transform.up * (capCol.height-offset) - transform.up * radius;
        Collider[] outputCols = Physics.OverlapCapsule(point1, point2, radius,LayerMask.GetMask("Ground"));
        if (outputCols.Length!=0)
        {
            SendMessageUpwards("IsGround");
        }
        else
        {
            SendMessageUpwards("IsNotGround");
        }
    }
}

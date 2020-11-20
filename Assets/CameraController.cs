using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    private GameObject playerHandle;
    private GameObject cameraHandle;
    public float horizontalSpeed = 100;
    public float verticalSpeed = 80;
    public float cameraDampValue = 0.05f;
    public PlayerInput pi;

    private float tempEulerX;
    private GameObject model;
    private GameObject camera;

    private Vector3 cameraDampVelocity;

	// Use this for initialization
	void Awake () {
        cameraHandle = transform.parent.gameObject;
        playerHandle = cameraHandle.transform.parent.gameObject;
        tempEulerX = 20;

        model = playerHandle.GetComponent<ActorController>().model;
        camera = Camera.main.gameObject;
    }
	
	// Update is called once per frame
	void FixedUpdate () {


        Vector3 tempModelEuler = model.transform.eulerAngles;
        playerHandle.transform.Rotate(Vector3.up, pi.Jright  * Time.fixedDeltaTime* horizontalSpeed);

        tempEulerX -= pi.Jup * Time.fixedDeltaTime * verticalSpeed;
        tempEulerX = Mathf.Clamp(tempEulerX, -40, 30);

        cameraHandle.transform.localEulerAngles = new Vector3(tempEulerX, 0, 0);
        model.transform.eulerAngles = tempModelEuler;

        //camera.transform.position = Vector3.Lerp(camera.transform.position, transform.position, 0.2f);
        camera.transform.eulerAngles = transform.eulerAngles;
        camera.transform.position = Vector3.SmoothDamp(camera.transform.position, transform.position, ref cameraDampVelocity, cameraDampValue);
    }
}

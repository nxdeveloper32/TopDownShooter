using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent (typeof (PlayerController))]
[RequireComponent(typeof(GunController))]
public class PlayerInput : LivingEntity {
    public float moveSpeed = 3;
    PlayerController controller;
    GunController gunController;
    Camera ViewCamera;
    // Use this for initialization
    protected override void Start () {
        base.Start();
        controller = GetComponent<PlayerController>();
        ViewCamera = Camera.main;
        gunController = GetComponent<GunController>();
    }
	
	// Update is called once per frame
	void Update () {
        //Move Input
        Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        Vector3 moveVelocity = moveInput.normalized * moveSpeed;
        controller.Move(moveVelocity);
        //Look Input
        Ray ray = ViewCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayDistance;
        if (groundPlane.Raycast(ray, out rayDistance))
        {
            Vector3 point = ray.GetPoint(rayDistance);
            //Debug.DrawLine(ray.origin, point, Color.red);
            controller.lookat(point);
        }
        //Weapon Input
        if (Input.GetMouseButton(0))
        {
            gunController.Shoot();
        }
    }
}

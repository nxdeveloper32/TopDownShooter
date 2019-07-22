using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent (typeof (Rigidbody))]
public class PlayerController : MonoBehaviour {
    Rigidbody rb;
    Vector3 velocity;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        rb.MovePosition(rb.position + velocity * Time.deltaTime);
	}
    public void Move(Vector3 _velocity)
    {
        velocity = _velocity;
    }
    public void lookat(Vector3 lookat)
    {
        Vector3 HeightCorrected = new Vector3(lookat.x, transform.position.y, lookat.z);
        transform.LookAt(HeightCorrected);
    }
}

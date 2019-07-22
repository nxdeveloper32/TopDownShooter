using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumping : MonoBehaviour {
    public float jumpforce;
   public float angle;
    public Transform origin;
    public Transform EndPosition;
    int resolution = 10;
    public float offset;
    public Rigidbody ball;
    public Vector3 test;
    int angledir;
    LineRenderer lr;
    //New

    public float Velocity;
    float radianAngle;
    float gravity;
    // Use this for initialization
    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
        gravity = Mathf.Abs(Physics.gravity.y);
    }
    void Start () {
        if (lr != null && Application.isPlaying)
        {
            DrawArc();
        }
        
    }

    // Update is called once per frame
    private void Update()
    {
        if(angle <= 0)
        {
            angledir = 5;
        }
        if(angle >= 360)
        {
            angle = 0;
        }
        
        DrawArc();
        if (Input.GetMouseButton(0))
        {
           
        }
        else
        {
            angle += angledir;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Launch();
        }
    }
    void Launch()
    {
        
        ball.velocity = test;
        ball.useGravity = true;
    }
    void OnValidate() {
        if (lr != null && Application.isPlaying)
        {
            DrawArc();
        }
//Debug.DrawLine(origin.position, Target, Color.green);

    }
    void DrawArc()
    {
        lr.SetVertexCount(resolution + 1);
        lr.SetPositions(Calculation());
    }
    Vector3[] Calculation()
    {
        Vector3[] arc = new Vector3[resolution + 1];
        float maxDistance = (jumpforce * jumpforce)/ gravity;
        Vector3 Target = new Vector3(Mathf.Sin(Mathf.Deg2Rad * angle)* maxDistance, 0, Mathf.Cos(Mathf.Deg2Rad * angle)* maxDistance);
        
        for (int i = 0; i <= resolution; i++)
        {
            float time = (float)i / (float)resolution;
            float y = i * Mathf.Tan(radianAngle) - ((gravity * i * i) / (2 * jumpforce * jumpforce * Mathf.Cos(radianAngle) * Mathf.Cos(radianAngle)));
            //float y =  Mathf.Sqrt(2 * gravity * jumpforce);
            Debug.Log(y);
            test = new Vector3(Target.x * time,y + offset, Target.z * time)  ;
            arc[i] = test;
            //lr.SetPosition(i, );
        }
        return arc;
    }
    //Vector3[] CalculateArcArray()
    //{
    //    Vector3[] arcArray = new Vector3[resolution + 1];
    //    radianAngle = Mathf.Deg2Rad * angle;
    //    float maxDistance = (Velocity * Velocity * Mathf.Sin(2 * radianAngle)) / gravity;
    //    for (int i = 0; i <= resolution; i++)
    //    {
    //        float t = (float)i / (float)resolution;
    //        arcArray[i] = CalculateArcPoint(t, maxDistance);
    //    }
    //    return arcArray;
    //}
    ////calculate height and distance of arc
    //Vector3 CalculateArcPoint(float t, float maxDistance)
    //{
    //    float x = t * maxDistance;
    //    float y = x * Mathf.Tan(radianAngle) - ((gravity * x * x) / (2 * Velocity * Velocity * Mathf.Cos(radianAngle) * Mathf.Cos(radianAngle)));
    //    float z = t * maxDistance;
    //    return new Vector3(x, y);
    //}
}

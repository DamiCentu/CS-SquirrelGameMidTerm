using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoventOnTree : MonoBehaviour, IMoventOnSurface {

    public Rigidbody rb;
    public Transform playerTransform;
    public float maxDistanceClimbingArea;
    public float minDistanceClimbingArea;
    public float climbingVelocity=0;

    public float currentVelocity { get; private set; }



    void IMoventOnSurface.Move()
    {
        float ver = Input.GetAxis("Vertical");
        float hor = Input.GetAxis("Horizontal");
        Vector3 direction = new Vector3(hor, ver, rb.velocity.z).normalized;

        direction = playerTransform.TransformVector(direction);
        //    rb.AddForce(this.transform.forward, ForceMode.Force);
        RotateToFollowTree();
        rb.MovePosition(direction * climbingVelocity*Time.deltaTime + rb.position);
        //print("asd");

        rb.velocity = Vector3.zero;
       // rb.useGravity = false;
        currentVelocity = direction.magnitude;
        /*   Vector3 final_velocity = rb.velocity;
           final_velocity.y = 0;
           rb.velocity = final_velocity;
           currentVelocity = final_velocity.magnitude;*/
    }


    private void RotateToFollowTree()
    {
        RaycastHit rayHit = new RaycastHit();
        if (Physics.Raycast(playerTransform.position, playerTransform.forward, out rayHit, maxDistanceClimbingArea, PlayerBrain.instance.ClimbableLayers))
        {

            playerTransform.rotation = Quaternion.LookRotation(rayHit.normal * -1);
            //this.transform.position = rayHit.point;
            Debug.DrawRay(rayHit.point, rayHit.normal, Color.green);
            Vector3 position_piola = playerTransform.position - playerTransform.forward;
            float distanceToAproach = (Vector3.Distance(position_piola, rayHit.transform.position) - minDistanceClimbingArea);
            //   distanceToAproach = 5f;

            if (distanceToAproach > 0)
            {
                Debug.DrawLine(rb.position , playerTransform.forward * distanceToAproach + rb.position + new Vector3(0, 1, 0), Color.cyan);
                this.rb.MovePosition((rayHit.normal * -1) / 20 + rb.position);
                //     this.rb.AddForce((rayHit.normal*-1 ),ForceMode.Force );
            }

        }

    }


    void IMoventOnSurface.Active()
    {
        
    }

    Vector3 IPositionPredictable.currentDirection()
    {
        return Vector3.zero;
    }
}

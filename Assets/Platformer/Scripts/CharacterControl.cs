using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    public float maxSpeed = 10f;
    public float speed = 10f;
    public float jumpImpulse = 50f;
    public float jumpBoost = 3f;
    public bool isGrounded = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalMovement = Input.GetAxis("Horizontal");
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity += horizontalMovement * Time.deltaTime * speed * Vector3.right;
        // rb.velocity *= Math.Abs(horizontalMovement);

        Collider col = GetComponent<Collider>();
        float halfHeight = col.bounds.extents.y + 0.03f;

        Vector3 startPoint = transform.position;
        // Vector3 endPoint = startPoint + Vector3.down * halfHeight;

        isGrounded = Physics.Raycast(startPoint, Vector3.down, halfHeight);

        if (Math.Abs(rb.velocity.x) > maxSpeed)
        {
            float newXVal = Math.Clamp(rb.velocity.x, -maxSpeed, maxSpeed);
            rb.velocity = new Vector3(newXVal, 0f, 0f);
        }
        
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpImpulse, ForceMode.Impulse);
            Debug.Log("jump");
        }
        else if (!isGrounded && Input.GetKey(KeyCode.Space) && rb.velocity.y > 0)
        {
            rb.AddForce(Vector3.up * jumpBoost, ForceMode.Force);
        }

        if (isGrounded && Math.Abs(horizontalMovement) < 0.5f)
        {
            Vector3 newVel = rb.velocity;
            newVel.x *= 1f - Time.deltaTime;
            rb.velocity = newVel;
        }
    }
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Climbing;

public class upwardDash : MonoBehaviour
{
    public float dashForce = 10f;       // The force applied for the upward dash
    public float dashDuration = 0.2f;   // The duration of the dash
    private bool isDashing = false;     // Flag to track if the object is currently dashing

    private ThirdPersonController controller;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        controller = GetComponent<ThirdPersonController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G) && !isDashing && controller.isGrounded)
        {
            DashUpward();
        }
    }

    private void DashUpward()
    {
        isDashing = true;
        controller.isGrounded = false;  // Accessing isGrounded through the controller instance

        // Apply upward force to the Rigidbody
        rb.AddForce(Vector3.up * dashForce, ForceMode.Impulse);

        // Start a coroutine to end the dash after a certain duration
        StartCoroutine(EndDash());

        Debug.Log("Dashing");
    }

    private IEnumerator EndDash()
    {
        // Wait for the specified dash duration
        yield return new WaitForSeconds(dashDuration);

        // Disable the dashing flag to allow another dash
        isDashing = false;
    }
}
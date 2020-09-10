﻿using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Singleton Instance
    public static PlayerController Instance { get; private set; }

    [HideInInspector] public PlayerUnit unit;
    [HideInInspector] public CharacterController controller;

    private Vector3 velocity;
    private float baseSpeed;
    private float sprintSpeed;


    private void Start()
    {
        unit = GetComponent<PlayerUnit>();
        controller = GetComponent<CharacterController>();
        baseSpeed = unit.stats.moveSpeed;
        sprintSpeed = unit.stats.moveSpeed * 2f;
    }

    private void Update()
    {
        Movement();
    }

    private void Movement()
    {
        Vector3 move = transform.right * Input.GetAxis("Horizontal") + transform.forward * Input.GetAxis("Vertical");
        Vector3 groundVec = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - gameObject.transform.localScale.y, gameObject.transform.position.z);

        bool isGrounded = Physics.CheckSphere(groundVec, unit.stats.groundDistance, unit.stats.groundMask);      

        controller.Move(move * unit.stats.moveSpeed * Time.deltaTime);

        // Ground Check
        if (isGrounded && velocity.y < 0){
            velocity.y = -2f;
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(unit.stats.jumpHeight * -2f * unit.stats.gravity);
        }

        if (Input.GetButton("Sprint")) {
            unit.stats.moveSpeed = sprintSpeed;
        }
        else {
            unit.stats.moveSpeed = baseSpeed;
        }

        velocity.y += unit.stats.gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
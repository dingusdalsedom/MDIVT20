using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController controller;
    public float speed = 12f;
    public float gravity = -9.82f;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;
    void Start()
    {
        var pod = new POD();
        var pod2 = new POD();
        pod.addTimedObject("test1");
        pod.addTimedObject("test2");
        pod.addTimedObject("test3");
        pod.addTimedObject("test4");
        pod.addTimedObject("test5");
        pod.addTimedObject("test1");
        pod.addTimedObject("test2");
        pod.addTimedObject("test3");
        pod.addTimedObject("test4");
        pod.addTimedObject("test5");
        //pod.stopTimer();
        CSV.WriteSequential("Assets/Scenes/TestScene/Resources/CSVSeq.txt", pod);
        CSV.ReadSequential("Assets/Scenes/TestScene/Resources/CSVSeq.txt", pod2);
        CSV.WriteSummary("Assets/Scenes/TestScene/Resources/CSVSum.txt", pod);
    }
    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);


        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingWithJohn : MonoBehaviour
{
    public float turnSpeed = 20;
    public float moveSpeed = 10f;
    public float JumpForce = 10f;
    public float gravityModifier =1f;
    public bool isOnGround = true;
    private Vector3 _movement;
    //private Animator m_Animator;
    private Rigidbody _rigidbody;
    private Quaternion _rotation = Quaternion.identity;

    // Start is called before the first frame update
    void Start()
    {
        //m_Animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            _rigidbody.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
            isOnGround = false;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        _movement.Set(horizontal, 0f, vertical);
        _movement.Normalize();

        bool hasHorizontalInput = !Mathf.Approximately (horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately (vertical, 0f);
        bool isWalking = hasHorizontalInput || hasVerticalInput;

        Vector3 desiredForward = Vector3.RotateTowards (transform.forward, _movement, turnSpeed * Time.deltaTime, 0f);
        _rotation = Quaternion.LookRotation (desiredForward);
    
        _rigidbody.MovePosition (_rigidbody.position + _movement * moveSpeed * Time.deltaTime);
        _rigidbody.MoveRotation (_rotation);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }
    }
}
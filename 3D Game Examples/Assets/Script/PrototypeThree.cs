using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prototypehree : MonoBehaviour
{
    public float horizontalInput;
    private float verticalInput;
    private Rigidbody _playerRb;
    public float JumpForce = 10f;
    public float Speed = 1f;
    public float gravityModifier =1f;
    public bool isOnGround = true;
    public bool gameOver = false;
    // Start is called before the first frame update
    void Start()
    {
        _playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        if(Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
        {
            _playerRb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
            isOnGround = false;
        }

        Vector3 movement = new Vector3(horizontalInput, 0.0f, verticalInput);
        _playerRb.AddForce(movement * Speed);
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }
    }
}

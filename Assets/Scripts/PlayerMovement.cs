//
//  The MIT License (MIT)
//  Copyright © 2020 d-exclaimation
//

using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // NOTE:  Unity Accessible Variables
    public Rigidbody2D playerRigidbody2D;
    public float walkSpeed = 100f;
    public float jumpSpeed = 30f;

    public Animator animator;
    public LayerMask groundLayers;
    public Transform feet;
    public AudioManager audioManager;
    [HideInInspector] public bool isFacingRight;

    // NOTE: Private reference properties
    private float _mx;
    private float _defaultGravity;
    private float _timeTillJump;
    private Vector2 jumpMovement => new Vector2(playerRigidbody2D.velocity.x , jumpSpeed);
    private GameManager manager => FindObjectOfType<GameManager>();

    private const float Compensation = 0.005f;
    private const float JumpRate = 0.4f;


    // NOTE:  Update is called once per frame
    private void Update() {

        if (_defaultGravity == 0f) {
            // To compensate for changing the gravity later
            _defaultGravity = playerRigidbody2D.gravityScale;
        }

        // Get a variable represent the x direction axis of input
        _mx = Input.GetAxisRaw("Horizontal");
        
        // Possible animation parameters
        string[] parameters = {"isRunning", "isGrounded"};

        // Set player gravity to default given constant
        playerRigidbody2D.gravityScale = _defaultGravity;

        // if player is moving then animate running
        if(Math.Abs(_mx) > Compensation) {
            animator.SetBool(parameters[0], true); 
        } else {
            animator.SetBool(parameters[0], false);
        }

        // Check which orietation should the player be
        if (_mx > Compensation) {
            transform.localScale = new Vector3(1f, 1f, 1f);
            isFacingRight = true;
        } else if(_mx < -Compensation) {
            transform.localScale = new Vector3(-1f, 1f, 1f);
            isFacingRight = false;
        }

        // Activate jumping animation if player is not grounded
        animator.SetBool(parameters[1], isGrounded());
        
        // Menu
        if (Input.GetKey(KeyCode.Escape)) {
            manager.loadMenu();
        }
    }

    private void FixedUpdate() {

        // Create a new velocity for the player
        var movement = new Vector2(_mx * walkSpeed, playerRigidbody2D.velocity.y);

        // Crouch safe mode
        if(Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.C) || Input.GetKey(KeyCode.LeftShift)) {

            // Slowdown player
            movement = new Vector2(_mx * walkSpeed / 2, playerRigidbody2D.velocity.y);

            // Immediate rotation adjustment
            if (!isGrounded()) {
                playerRigidbody2D.transform.rotation = new Quaternion(0, 0, 0, 1);
            }

            // Slow falling
            if(playerRigidbody2D.velocity.y < 0) {
                playerRigidbody2D.gravityScale = _defaultGravity / 2;
            }

        }

        // Check if not able to move
        if (playerRigidbody2D.velocity == new Vector2(0, 0) && !isNormal()) {
            fixJump();
        }

        // Check if player is on a platform
        if (!isGrounded()) return;
        
        // Move the player by changing the velocity
        playerRigidbody2D.velocity = movement;

        // Jumping input
        if (Input.GetKey(KeyCode.Space) && _timeTillJump < Time.time) {
            jump();
            _timeTillJump = Time.time + JumpRate;
        }
    }

    private void jump() {

        // Play audio
        audioManager.playSound(AudioManager.SoundEffect.Jump);

        // Apply force as impulse
        playerRigidbody2D.velocity = jumpMovement;
    }

    private void fixJump() {

        // Play audio
        audioManager.playSound(AudioManager.SoundEffect.Jump);

        // Apply force as impulse and fix rotation
        playerRigidbody2D.velocity = jumpMovement;
        playerRigidbody2D.transform.rotation = new Quaternion(0, 0, 0, 1);
    }


    private bool isGrounded() {

        // Get the collider overlap for feet
        var groundCheck = Physics2D.OverlapCircle(feet.position, 0.5f, groundLayers);

        // Return true if there are overlap
        return groundCheck;
    }

    public bool isNormal() {
        var spinRotation = transform.rotation.z;
        return spinRotation >= -Compensation && spinRotation <= Compensation;
    }
}


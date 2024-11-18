using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

namespace MiniGameCollection.Games2024.Team15
{
    public class Player1Move : MonoBehaviour
    {
        // Movement variables
        public int lives = 3;  // Default life count for Player1
        public float moveSpeed = 5f;
        private Vector3 movement;
        private Rigidbody rb;

        // Jump variables
        public float jumpHeight = 8f;
        private bool isGrounded;
        public LayerMask groundLayer;

        // Stun variables
        private bool isStunned = false;
        public int stunsLeft = 3;  // Default stuns left
        public TMP_Text stunUI;  // Reference to the UI Text for stuns left
        private float stunDuration = 3f;

        // Reference to Player2's script
        public Player2Move player2Script;

        void Start()
        {
            rb = GetComponent<Rigidbody>();
            rb.freezeRotation = true;  // Prevent unintended rotations during movement
        }

        void Update()
        {
            // Update UI with stuns left
            stunUI.text = "Stuns left: " + stunsLeft;

            // Player 1 stun logic (Shift key for Player 1 to stun Player 2)
            if (Input.GetKeyDown(KeyCode.LeftShift) && !isStunned && stunsLeft > 0)
            {
                Debug.Log("Player 1 is using stun on Player 2!");
                player2Script.ApplyStun(stunDuration);  // Stun Player 2
                stunsLeft--;
            }

            // Prevent Player 1 from moving while stunned
            if (isStunned) return;

            // Jumping logic for Player 1
            if (ArcadeInput.Player1.Action1.Pressed && isGrounded)
            {
                rb.AddForce(new Vector3(rb.velocity.x, jumpHeight, rb.velocity.z), ForceMode.Impulse);
            }
        }

        void FixedUpdate()
        {
            // If stunned, don't allow movement
            if (isStunned) return;

            float h = ArcadeInput.Player1.AxisX;
            float v = ArcadeInput.Player1.AxisY;

            // If there's movement input, move the player
            if (h != 0 || v != 0)
            {
                Move(h, v);
            }
        }

        void Move(float hSpeed, float vSpeed)
        {
            // Calculate movement vector based on input axes
            movement = (transform.forward * vSpeed) + (transform.right * hSpeed);
            movement = movement.normalized * moveSpeed * Time.deltaTime;

            // Move the player with the Rigidbody
            rb.MovePosition(transform.position + movement);
        }

        // Collision detection for grounded state
        void OnCollisionStay(Collision collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                isGrounded = true;
            }
            else
            {
                isGrounded = false;
            }
        }

        // Remove Player 1's stun after the set duration
        public void RemoveStun()
        {
            Debug.Log("Player 1's stun is removed!");
            isStunned = false;
        }

        // Apply stun effect to Player 1
        public void ApplyStun(float duration)
        {
            Debug.Log("Player 1 is stunned!");
            isStunned = true;
            Invoke("RemoveStun", duration);
        }
    }
}
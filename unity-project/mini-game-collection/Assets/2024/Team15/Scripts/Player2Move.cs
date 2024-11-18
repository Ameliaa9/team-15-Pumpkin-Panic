using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace MiniGameCollection.Games2024.Team15
{
    public class Player2Move : MonoBehaviour
    {
        // Movement variables
        public int lives = 3;  // Default life count for Player2
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

        // Reference to Player1's script
        public Player1Move player1Script;

        void Start()
        {
            rb = GetComponent<Rigidbody>();
            rb.freezeRotation = true;
        }

        void Update()
        {
            // Update UI with stuns left
            stunUI.text = "Stuns left: " + stunsLeft;

            // Player 2 stun logic (Enter key for Player 2 to stun Player 1)
            if (Input.GetKeyDown(KeyCode.Return) && !isStunned && stunsLeft > 0)
            {
                Debug.Log("Player 2 is using stun on Player 1!");
                player1Script.ApplyStun(stunDuration);  // Stun Player 1
                stunsLeft--;
            }

            // Prevent Player 2 from moving while stunned
            if (isStunned) return;

            // Jumping logic for Player 2
            if (ArcadeInput.Player2.Action1.Pressed && isGrounded)
            {
                rb.AddForce(new Vector3(rb.velocity.x, jumpHeight, rb.velocity.z), ForceMode.Impulse);
            }
        }

        void FixedUpdate()
        {
            // If stunned, don't allow movement
            if (isStunned) return;

            float h = ArcadeInput.Player2.AxisX;
            float v = ArcadeInput.Player2.AxisY;

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

        // Remove Player 2's stun after the set duration
        public void RemoveStun()
        {
            Debug.Log("Player 2's stun is removed!");
            isStunned = false;
        }

        // Apply stun effect to Player 2
        public void ApplyStun(float duration)
        {
            Debug.Log("Player 2 is stunned!");
            isStunned = true;
            Invoke("RemoveStun", duration);
        }

        // Reset Player 2's stun state
        public void ResetStunState()
        {
            isStunned = false; // Ensure the player is no longer stunned
            stunsLeft = 3;     // Reset stuns to default
            stunUI.text = "Stuns left: " + stunsLeft; // Update UI
            Debug.Log("Player 2 stun state reset.");
        }
    }
}
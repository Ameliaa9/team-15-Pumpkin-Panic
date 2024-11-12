using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiniGameCollection.Games2024.Team15
{
    public class Player2MovementScript : MonoBehaviour
    {
        public float speed = 5f;
        public LayerMask groundLayerMask;
        public Transform groundCheckPlayer2; // Reference to "GroundCheckPlayer2" empty object just below player
        public float groundCheckRadius = 0.1f;
        private bool isGrounded;
        private bool isMovementLocked = false; // Movement lock flag

        void Update()
        {
            // Prevent movement during collision if locked
            if (isMovementLocked) return;

            // Ground check
            isGrounded = Physics.CheckSphere(groundCheckPlayer2.position, groundCheckRadius, groundLayerMask);

            Debug.Log("Is Grounded: " + isGrounded);

            // Player movement only if grounded
            if (isGrounded)
            {
                // Using P2_AxisX and P2_AxisY for Player 2 input
                float moveHorizontal = Input.GetAxis("P2_AxisX");
                float moveVertical = Input.GetAxis("P2_AxisY");

                // Move the player
                Vector3 move = new Vector3(moveHorizontal, 0.0f, moveVertical);
                transform.Translate(move * speed * Time.deltaTime);
            }
        }

        void OnCollisionEnter(Collision collision)
        {
            // Lock movement during collision
            isMovementLocked = true;
            StartCoroutine(UnlockMovementAfterDelay(0.2f)); // Adjust delay time as needed
        }

        IEnumerator UnlockMovementAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            isMovementLocked = false;
        }
    }
}
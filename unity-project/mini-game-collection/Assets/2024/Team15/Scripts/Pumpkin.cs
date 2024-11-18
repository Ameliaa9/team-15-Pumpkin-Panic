using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiniGameCollection.Games2024.Team15
{
    public class Pumpkin : MonoBehaviour
    {
        private void OnCollisionEnter(Collision collision)
        {
            

            // Check if the pumpkin hits Player1
            if (collision.gameObject.CompareTag("Player1"))
            {
                // Access Player1's script and decrease lives
                Player1Move player1 = collision.gameObject.GetComponent<Player1Move>();
                if (player1 != null)
                {
                    player1.lives--; // Decrease Player1's life
                    Destroy(gameObject); // Destroy the pumpkin upon collision
                }
            }

            // Check if the pumpkin hits Player2
            if (collision.gameObject.CompareTag("Player2"))
            {
                // Access Player2's script and decrease lives
                Player2Move player2 = collision.gameObject.GetComponent<Player2Move>();
                if (player2 != null)
                {
                    player2.lives--; // Decrease Player2's life
                    Destroy(gameObject); // Destroy the pumpkin upon collision
                }
            }

            // Check if the pumpkin hits the ground
            if (collision.gameObject.CompareTag("Ground"))
            {
                Destroy(gameObject); // Destroy the pumpkin when it hits the ground
            }
        }
    }
}

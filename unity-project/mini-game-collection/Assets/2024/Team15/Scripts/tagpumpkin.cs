using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiniGameCollection.Games2024.Team15
{
    public class Pumpkin : MonoBehaviour
    {
        private void OnCollisionEnter(Collision collision)
        {
            // Debug log to see what is colliding with the pumpkin
            Debug.Log("Collided with: " + collision.gameObject.name);

            // Check if Player 1's tag is attached
            bool isPlayer1 = collision.gameObject.GetComponentInChildren<tagplayer>() != null;
            if (isPlayer1)
            {
                Debug.Log("Pumpkin hit Player 1");
                ScoreTracker.Instance.Player1LoseLife();  // Call Singleton method
                Destroy(gameObject);  // Destroy the pumpkin
            }

            // Check if Ground's tag is attached
            bool isGround = collision.gameObject.GetComponentInChildren<tagground>() != null;
            if (isGround)
            {
                Debug.Log("Pumpkin hit the Ground");
                Destroy(gameObject);  // Destroy the pumpkin
            }
        }
    }
}
using System;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;
    
    // Reference properties
    private Vector3 selfPosition => transform.position;
    private Vector3 playerPosition => player.position;
    private float playerX => player.position.x;
    private float playerY => player.position.y;


    private void Start() {
        
        // Play starting at player
        transform.position = playerPosition;
    }

    // Update is called once per frame
    private void Update() {
        // Get current position of x and y
        var currX = selfPosition.x;
        var currY = selfPosition.y - 3;

        // Check if position is out of bubble from the player
        if(Math.Abs(player.position.x - currX) >= 0.5) {
            currX += 0.1f * (playerX - currX) / Math.Abs(playerX - currX);
        }

        if (Math.Abs(playerPosition.y - currY) >= 0.5) {
            currY += 0.2f * (playerY - currY) / Math.Abs(playerY- currY);
        }

        transform.position = new Vector3(currX, currY + 3, -10);
    }
}

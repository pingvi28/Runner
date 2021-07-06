using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public CapsMovement movement;

    void OnCollisionEnter (Collision collisionInfo) 
    {
        if (collisionInfo.collider.tag == "Obstacle") 
        {
            movement.enabled = false;
        }
    }
}


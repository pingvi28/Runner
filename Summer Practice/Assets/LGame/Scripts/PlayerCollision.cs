using UnityEngine;

public class PlayerCollision : MonoBehaviour {

	public CapsMovement movement;     // A reference to our PlayerMovement script

	/*
		void OnCollisionEnter (Collision collisionInfo)
		{
			// We check if the object we collided with has a tag called "Obstacle".
			if (collisionInfo.collider.tag == "Obstacle")
			{
				movement.enabled = false;   // Disable the players movement.
				FindObjectOfType<GameManager>().EndGame();
			}
		}*/

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Obstacle"))
		{
			movement.enabled = false;   // Disable the players movement.
			FindObjectOfType<GameManager>().EndGame();
		}
	}
}

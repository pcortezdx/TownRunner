using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    
    public float forceUpValue = 10f;
    public float gravityModifier = 1f;
    public bool isOnGround = true;
    public bool gameOver = false;

    void Start()
    {
        /**Because RigidBody is not a component that comes  
         * in every GameObject like the Transform, instead is a 
         * component that we add.Therefore, we need to 
         * use GetComponent<Component_type> 
        **/
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
        
    }

    void Update()
    {
        // Add an instant force (impulse) to move the player
        // when the user hits the spacebar.
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            playerRb.AddForce(Vector3.up * forceUpValue, ForceMode.Impulse);
            isOnGround = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Using tags to differentiate the collisions
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        } else if (collision.gameObject.CompareTag("Obstacle"))
        {
            gameOver = true;
            Debug.Log("Game Over!");
        }
        
    }
}

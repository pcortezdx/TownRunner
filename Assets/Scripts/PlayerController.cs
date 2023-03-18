using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private Animator playerAnimator;

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

        // Getting the animation component attached to the player
        playerAnimator = GetComponent<Animator>();

    }

    void Update()
    {
        // Add an instant force (impulse) to move the player
        // when the user hits the spacebar.
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
        {
            playerRb.AddForce(Vector3.up * forceUpValue, ForceMode.Impulse);
            isOnGround = false;

            //The animator parameters Jump_trig controls is used to
            // trigger the jump animation.
            playerAnimator.SetTrigger("Jump_trig");
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

            //Changing parameters for Death animation
            playerAnimator.SetBool("Death_b", true);
            playerAnimator.SetInteger("DeathType_int", 1);
        }
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private Animator playerAnimator;

    public ParticleSystem explotionParticle;
    public ParticleSystem dirtParticle;

    public AudioClip jumpSound;
    public AudioClip crashSound;
    private AudioSource playerAudio;
    private AudioSource backgroundAudio;

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

        //Getting the Audio Source component that is attached to the player
        // It will be used to control the Audio clip effects.
        playerAudio = GetComponent<AudioSource>();

        backgroundAudio = GameObject.Find("Main Camera").GetComponent<AudioSource>();
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
            dirtParticle.Stop();

            //Using PlayOneShot function, we play the audio clip 1 time
            // and set the sound scale to max volume 1
            playerAudio.PlayOneShot(jumpSound, 1.0f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Using tags to differentiate the collisions
        if (collision.gameObject.CompareTag("Ground") && !gameOver)
        {
            isOnGround = true;
            dirtParticle.Play();

        } else if (collision.gameObject.CompareTag("Obstacle"))
        {
            gameOver = true;
            Debug.Log("Game Over!");

            //Executing the appropiate particle effects
            dirtParticle.Stop();
            explotionParticle.Play();

            //Using PlayOneShot function, we play the audio clip 1 time
            // and set the sound scale to max volume 1
            playerAudio.PlayOneShot(crashSound, 1.0f);            

            //Changing parameters for Death animation
            playerAnimator.SetBool("Death_b", true);
            playerAnimator.SetInteger("DeathType_int", 1);

            //Avoid that the player is dead on air when it hits the
            //top of the obstacle.
            if (transform.position.y > 1.0)
            {
                transform.Translate(
                    new Vector3(transform.position.x, 0, transform.position.z), 
                    Space.World);
            }

            backgroundAudio.Stop();
        }
        
    }
}

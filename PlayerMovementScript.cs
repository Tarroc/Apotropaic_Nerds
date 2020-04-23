using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{

    public CharacterController controller;

    public float Stamina = 100f;
    public float MaxStamina = 100f; //This is for regeneration purposes so the stamina should work properly

    private float StaminaRegenTimer = 0f;

    public float speed;
    public float sprintspeed = 20f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    //Lives 
    public int PlayerLives = 3;
    //public Text LiveStatus;

    bool hasItem1;
    bool hasItem2;
    bool hasItem3;
    bool hasItem4;
    bool hasitem5;

    private const float StaminaDecreasePerFrame = 1.0f;
    private const float StaminaIncreasePerFrame = 5.0f;
    private const float StaminaTimeToRegen = 3.0f;

    Vector3 velocity;


    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    bool isGrounded;
    bool isRunning = false;


    // Update is called once per frame
    void Update()
    {
        PlayerLives = 3;
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        /*if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isRunning = true;
            speed = sprintspeed;
        }
        else
        {
            isRunning = false;
            speed = 12f;
        }*/
        //Stamina Section - 
        /*bool isSprinting = Input.GetKey(KeyCode.LeftShift);
        if (isRunning)
        {
            Stamina = Mathf.Clamp(Stamina - (StaminaDecreasePerFrame * Time.deltaTime), 0.0f, MaxStamina);
            StaminaRegenTimer = 0.0f;
        }
        else if (Stamina < MaxStamina)
        {
            if (StaminaRegenTimer >= StaminaTimeToRegen)
                Stamina = Mathf.Clamp(Stamina + (StaminaIncreasePerFrame * (StaminaTimeToRegen * Time.deltaTime), 0.0f, MaxStamina));
            else StaminaRegenTimer += StaminaRegenTimer.deltaTime;
        }*/
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

    }

    private void LifeSystem()
    {
        //LiveStatus.text = "Lives: " + PlayerLives.ToString;
        if (PlayerLives == 0)
            HealthDeathSystem();
    }

    private void OnTriggerEnter3D(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            PlayerLives = PlayerLives - 1;
            LifeSystem();
        }
    }

    private void HealthDeathSystem()
    {
        if (PlayerLives <= 0)
        {
            //LooseText.text = "I'm afraid you have been diagnosed with death, an ever so dangerous and uncurable disease";
            Destroy(gameObject);

        }
    }
}

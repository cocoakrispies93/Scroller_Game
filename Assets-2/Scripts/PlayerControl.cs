using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class PlayerControl : MonoBehaviour
{

    public AudioSource playerSound;
    public AudioClip breakClip, fireClip;
    //public static GameObject bullet;
    public GameObject bullet;

    //KeyCode attribute
    public KeyCode moveUp, moveDown, moveLeft, moveRight, fire;

    //public float speedX = 0, speedY = 0;
    public float speedX, speedY;
    public bool linearMovement = true;
    public Camera mainCam;
    public static int score;
    private Rigidbody2D rbody;
    public static int health;
    //public static float bulletTopScreen;
    //public static bullet_position = bullet.transform.position;
    //public Rigidbody2D bullet_body;
    //public GameObject Bullet;
    //public static Vector3 screenPos2 = mainCam.WorldToScreenPoint(transform.position);
    //public static Vector3 topScreen2 = mainCam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f));


    // b. Add an attribute to the player class called health and an
    // attribute to the critter class called healthValue.
    //
    // Then in both scenes (Level1 and Level2) set player's health to 1. Set the health
    // value of all the critters to 0 except for the rock to 1. You can set
    // it in the prefab for all critters.
    //
    // Then in the player's class proceed
    // to add or remove the health value of the critters to/from the player's
    // health based on whether they are friends or foes, just like it is done
    // with the score value.
    //
    // Add a test in the GameMaster to also end the game
    // when the player's health becomes 0 in level 2.




    public void Fire()
    {
        //playerSound.clip = fireClip;
        //playerSound.Play();
        //fireClip.Play();
        GameObject bullet2;
        bullet2 = Instantiate(bullet) as GameObject;
        //bullet.transform.position = transform.position;
        //bullet_position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
        bullet2.transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
        //bullet_position = bullet.transform.position;
        Debug.Log("Position of bullet is: " + bullet2.transform.position);

        // !!!!!!!!!!!!!!!! Not working
        playerSound.clip = fireClip;
        playerSound.Play();



        //In the function Fire, instantiate one object of type bullet.

        //bullet_body.AddForce(new Vector2(0f, 5f * speedY));
        //bullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 5f));
        //bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 5f);
        //bullet.AddForce(new Vector2(0f, 5f));

        //if (linearMovement) // simple constant velocity
        bullet2.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, speedY*2);
        Debug.Log("Velocity of bullet is: " + bullet2.GetComponent<Rigidbody2D>().velocity);
        //else // if we were going to use a force instead
        //{
            //if (bullet.GetComponent<Rigidbody2D>().velocity.y < 0.2f * speedY)
                //bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0.2f * speedY);
            //bullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, speedY));
        //}
    }


    // Initialization function
    void Start()
    {
        // store the rigid body in an attribute for easier access.
        rbody = GetComponent<Rigidbody2D>();
        //bullet_body = GetComponent<Rigidbody2D>();

        playerSound = GetComponent<AudioSource>();

        if (playerSound)
        {
            breakClip = playerSound.clip;
            fireClip = Resources.Load("fire") as AudioClip;
        }
    }

    // Update is called once per frame
    // move the player in the 4 directions based on the arrow keys
    void Update()
    {

        // move the player in the 4 directions based on the keys we set up for it
        if (Input.GetKey(moveUp))
        {
            if (linearMovement) // simple constant velocity
                rbody.velocity = new Vector2(0f, speedY);
            else // if we were going to use a force instead
            {
                if (rbody.velocity.y < 0.2f * speedY)
                    rbody.velocity = new Vector2(0f, 0.2f * speedY);
                rbody.AddForce(new Vector2(0f, speedY));
            }
        }

        else if (Input.GetKey(moveDown))
        {
            if (linearMovement)
                rbody.velocity = new Vector2(0f, -speedY);
            else
            {
                if (rbody.velocity.y > -0.2f * speedY)
                    rbody.velocity = new Vector2(0f, -0.2f * speedY);
                rbody.AddForce(new Vector2(0f, -speedY));
            }
        }

        else if (Input.GetKey(moveLeft))
        {
            if (linearMovement)
                rbody.velocity = new Vector2(-speedX, 0f);
            else
            {
                if (rbody.velocity.x > -0.2f * speedX)
                    rbody.velocity = new Vector2(-0.2f * speedX, 0f);
                rbody.AddForce(new Vector2(-speedX, 0f));
            }
        }

        else if (Input.GetKey(moveRight))
        {
            if (linearMovement)
                rbody.velocity = new Vector2(speedX, 0f);
            else
            {
                if (rbody.velocity.x < 0.2 * speedX)
                    rbody.velocity = new Vector2(0.2f * speedX, 0f);
                rbody.AddForce(new Vector2(speedX, 0f));
            }
        }

        //Add a case of the conditional for it in the function Update and call the function Fire
        else if (Input.GetKeyUp(fire))
        {
            //bullet.GetComponent<Rigidbody2D>().Fire();
            //bullet.Fire();
            Debug.Log("Input.GetKeyUp(fire) worked with the spacebar");
            Fire();
        }

        else
        {
            // no input, reset the speed
            rbody.velocity = new Vector2(0f, 0f);
        }

        AdjustPosition();      
    }

    // function to make sure the player doesn't go off the screen vertically
    // horizontal movement is circular, looping left-right or right-left 
    void AdjustPosition()
    {
        Vector3 screenPos = mainCam.WorldToScreenPoint(transform.position);
        Vector3 topScreen = mainCam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f));
        Vector3 bottomScreen = mainCam.ScreenToWorldPoint(new Vector3(0f, 0f, 0f));
        Vector3 leftScreen = mainCam.ScreenToWorldPoint(new Vector3(0f, Screen.height, 10f));
        Vector3 rightScreen = mainCam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f));

        // vertical adjustment
        if (screenPos.y > Screen.height)
            transform.position = new Vector3(transform.position.x, topScreen.y, transform.position.z);
        else if (screenPos.y < 0)
            transform.position = new Vector3(transform.position.x, bottomScreen.y, transform.position.z);

        // horizontal looping
        if (screenPos.x > Screen.width) //right
            transform.position = new Vector3(leftScreen.x, transform.position.y, transform.position.z);            
        else if (screenPos.x < 0) //left
            transform.position = new Vector3(rightScreen.x, transform.position.y, transform.position.z);
            
    }
}


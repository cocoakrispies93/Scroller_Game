using UnityEngine;
using System.Collections;

public class CritterControl : MonoBehaviour
{


    public Camera mainCam = null;
    public bool friend = false;
    public int scoreValue = 0;
    public bool respawn = true;
    AudioSource sound;
    public int healthValue;
    // public int health;

    // Randomizes the position and the scale of the object from Start into it.
    // Resets its velocity to 0 in this function.
    // Function Start calls this function, allows reuse
    public void Respawn()
    {
        if(gameObject.tag != "bullet")
        {
            // Randomize the initial position based on the screen size above the top of the screen
            float x = Random.Range(10, Screen.width - 9);
            float y = Screen.height + Random.Range(10, 2000);
            //float y =  Random.Range(Screen.height+10, Screen.height + 100);
            //float y = Random.Range(20, 100);
            float s = Random.Range(0.75f * transform.localScale.x, 1.25f * transform.localScale.x);

            // then covert it to world coordinates and assign it to the critter.
            Vector3 pos = mainCam.ScreenToWorldPoint(new Vector3(x, y, 0f));
            pos.z = transform.position.z;
            transform.position = pos;

            //z_scale = transform.localScale.z; // original z scale

            Vector3 scale = new Vector3(s, s, transform.localScale.z);
            transform.localScale = scale;
            //Vector3 scale = new Vector3(s, s, s);

            //Debug.Log("transform.localScale: " + transform.localScale);

            GetComponent<Rigidbody2D>().velocity = new Vector3(0f, 0f);
        }
    }



    // collision detection function
    // Note that if Collision2D colInfo is your parameter in a collision detection function,
    // then colInfo.gameObject is the game object attached to it.
    void OnCollisionEnter2D(Collision2D colInfo)
    {
        if (colInfo.collider.tag == "Player")
        {
            //respawn = true;

            if (friend == true)
            {
                if (GameMaster.level == 1)
                {
                    PlayerControl.score += scoreValue;
                    if (PlayerControl.score <= 0)
                    {
                        respawn = false;
                    }
                }

                else if (GameMaster.level == 2)
                {
                    GameMaster.critterCountDown -= 1;
                    //PlayerControl.health += scoreValue; //for friend val is 0
                    PlayerControl.score += scoreValue;

                    if (GameMaster.critterCountDown <= 0 || PlayerControl.health <= 0)
                    {
                        respawn = false;
                    }
                }
            }

            else if (friend == false)
            {
                if (GameMaster.level == 1)
                {
                    PlayerControl.score -= scoreValue;
                    if (PlayerControl.score <= 0)
                    {
                        respawn = false;
                    }
                }

                if (GameMaster.level == 2)
                {
                    PlayerControl.health -= scoreValue;
                    PlayerControl.score -= scoreValue;
                    if (GameMaster.critterCountDown <= 0 || PlayerControl.health <= 0)
                    {
                        respawn = false;
                    }
                }
            }

            if (respawn == true)
                Respawn();

            sound.Play();
        }

        else if (colInfo.collider.tag == "bullet")
        {
            //respawn = true;

            if (friend == true)
            {
                Debug.Log("collider tag is bullet, gameObject is: " + gameObject);
                Debug.Log("colInfo.gameObject is: " + colInfo.gameObject);
                Debug.Log("friend is true");
                if (colInfo.gameObject != null)
                {
                    Destroy(colInfo.gameObject);
                    Respawn();
                }
                    
            }

            else if (friend == false)
            {
                Debug.Log("collider tag is bullet, gameObject is: " + gameObject);
                Debug.Log("colInfo.gameObject is: " + colInfo.gameObject);
                Debug.Log("friend is false");

                if (colInfo.gameObject != null)
                {
                    Destroy(colInfo.gameObject);
                    Respawn();
                }
                    
            }
        }

        // Then add another case in the collision detection of
        // critters where in case they collide with the ground they respawn.
        // For all levels need to respawn critters if they hit the ground.
        if (respawn == true && colInfo.collider.tag == "Ground")
        {
            Respawn();
        }

    }





    // randomize the scale of the object in the range[0.75, 1.25], only x and y
    void Start()
    {
        // Find the camera from the object tagged as Player.
        if (!mainCam)
            mainCam = GameObject.FindWithTag("Player").GetComponent<PlayerControl>().mainCam;


        Respawn();
        sound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    //void Update () {

    //}


}

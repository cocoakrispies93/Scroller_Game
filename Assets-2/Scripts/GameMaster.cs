using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameMaster : MonoBehaviour
{

    public GameObject friendRef, foeRef;
    public GameObject[] friends;
    public GameObject[] foes;
    public GameObject moon;
    public GameObject winMsg;
    public GameObject scoreboard;
    public GameObject startGameTxt;
    public bool gameOver;
    public static int level;
    public int setLevel;
    public static int critterCountDown; // Countdown of friend critters left to catch in GameMaster


    void StartLevel()
    {
        Debug.Log("StartLevel() running!!");
        Debug.Log("Level is: " + level);

        if (level == 1)
        {
            PlayerControl.score = 50; //Start the score at 50
            //scoreboard.GetComponent<TextMesh>().text = "Player Score: " + PlayerControl.score;

            //Debug.Log("level == 1 if statement in StartLevel() entered");           

            for (int i = 0; i < 5; i++)
            {
                Debug.Log("We've entered for-loop to instantiate friends and foes");
                friends[i] = Instantiate(friendRef) as GameObject;
                foes[i] = Instantiate(foeRef) as GameObject;
                friends[i].SetActive(false);
                foes[i].SetActive(false);
            }                       
        }


        else if (level == 2)
        {
            critterCountDown = 10;
            PlayerControl.health = 100;
            //scoreboard.GetComponent<TextMesh>().text = "Critters Rescued: " + critterCountDown;

            Debug.Log("We've entered level 2");           

            for (int i = 0; i < 5; i++)
            {
                friends[i] = Instantiate(friendRef) as GameObject;
                foes[i] = Instantiate(foeRef) as GameObject;
                friends[i].SetActive(false);
                foes[i].SetActive(false);
            }
        }
    }

    // game master checks for the critter countdown or
    // the scoreboard becoming 0 in the function CheckEndLevel
    // for either level 1 or level 2.
    void CheckEndLevel()
    {
        if (level == 1 && PlayerControl.score <= 0)
        {
            gameOver = true;

            for (int i = 0; i < 5; i++)
            {
                    friends[i].SetActive(false);
                    foes[i].SetActive(false);
            }

            moon.SetActive(true);
            winMsg.GetComponent<TextMesh>().text = "\nLevel 1 Complete!! :D \n  Press Spacebar";           
        }


        if (level == 2)
        {       
            if (critterCountDown <= 0)
            {
                gameOver = true;

                for (int i = 0; i < 5; i++)
                {
                    friends[i].SetActive(false);
                    foes[i].SetActive(false);
                }

                moon.SetActive(true);
                winMsg.GetComponent<TextMesh>().text = "\n Level 2 Complete!! :D ";
            }
            
            else if (PlayerControl.health <= 0)
            {
                gameOver = true;

                for (int i = 0; i < 5; i++)
                {
                    friends[i].SetActive(false);
                    foes[i].SetActive(false);
                }

                moon.SetActive(true);
                winMsg.GetComponent<TextMesh>().text = "\n  Damn you suck :/ ";
            }
        }
    }


    // a.Add a countdown of friend critters left to catch to the
    // GameMaster class and every time the player collides with a
    // friend object (by using a tag, for example), decrease this
    // countdown by 1. Show it somewhere on screen.



    // Start is called before the first frame update
   void Start()
    {
        moon.SetActive(false);
        gameOver = false;
        level = setLevel;
        StartLevel();
    }

    // Update is called once per frame
    void Update()
    {
        // Then in the function Update in the class GameMaster, check for the
        // score/critter countdown is 0, and if it is, then deactivate all the
        // friends and foes and activate the moon object with a win message. 

        //scoreboard.GetComponent<TextMesh>().text = "Player Score: " + PlayerControl.score;
        if(level == 1)
            scoreboard.GetComponent<TextMesh>().text = "Player Score: " + PlayerControl.score;
        else if(level == 2)
            scoreboard.GetComponent<TextMesh>().text = "Rescues Needed: " + critterCountDown + "    Health: " + PlayerControl.health + "   Score: " + PlayerControl.score;

        if (Input.anyKey && gameOver == false)
        {
            startGameTxt.SetActive(false);
            startGameTxt.GetComponent<TextMesh>().text = "";

            for (int i = 0; i < 5; i++)
            {
                friends[i].SetActive(true);
                foes[i].SetActive(true);
            }                        
        }

        if (gameOver == true && level == 1)
        {
            if(Input.GetKey("space"))
                SceneManager.LoadScene("Level-2");        
        }
       
        if (gameOver == false)
            CheckEndLevel();


    }
}

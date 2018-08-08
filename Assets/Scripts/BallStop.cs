using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallStop : MonoBehaviour
{

    public Rigidbody2D ball;
    public BallController ballControl;
    private GameManager gameManager;
	// Use this for initialization
	void Start ()
	{
	    gameManager = FindObjectOfType<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ball")
        {
            //Stop ball
            ball.velocity = Vector2.zero;//new Vector2(0f, 0f);
            //Rotate the ball

            //reset the level
            //set the ball as active
            ballControl.currentBallState = BallController.ballState.wait;
        }
        if (other.gameObject.tag == "ExtraBall")
        {
            gameManager.ballsInScene.Remove(other.gameObject);
            other.gameObject.SetActive(false);
        }
    }
}

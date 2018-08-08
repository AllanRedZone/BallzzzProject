using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExtraBallManager : MonoBehaviour
{

    private BallController ballController;
    private GameManager gameManager;
    public float ballWaitTime;
    private float ballWaitTimeSecons;
    public int numberOfExtraBalls;
    public int numberOfBallsToFire;
    public ObjectPool objectPool;
    public Text numberOfBallsText;


	// Use this for initialization
	void Start ()
	{
	    ballController = FindObjectOfType<BallController>();
	    gameManager = FindObjectOfType<GameManager>();
	    ballWaitTimeSecons = ballWaitTime;
	    numberOfExtraBalls = 0;
	    numberOfBallsToFire = 0;
	    numberOfBallsText.text = "" + 1;
	}
	
	// Update is called once per frame
	void Update ()
	{
	    numberOfBallsText.text = "" + (numberOfExtraBalls + 1);
        if (ballController.currentBallState == BallController.ballState.fire || ballController.currentBallState == BallController.ballState.wait)
	    {
	        if (numberOfBallsToFire > 0)
	        {
	            ballWaitTimeSecons -= Time.deltaTime;
	            if (ballWaitTimeSecons <= 0)
	            {
	                GameObject ball = objectPool.GetPooledObject("ExtraBall");
	                if (ball != null)
	                {
	                    ball.transform.position = ballController.ballLounchPosition;
                        ball.SetActive(true);
                        gameManager.ballsInScene.Add(ball);
	                    ball.GetComponent<Rigidbody2D>().velocity = 12*ballController.tempVelocity;
	                    ballWaitTimeSecons = ballWaitTime;
	                    numberOfBallsToFire--;
	                }
	                ballWaitTimeSecons = ballWaitTime;
	            }
	        }
	    }
	    if (ballController.currentBallState == BallController.ballState.endShot)
	    {
	        numberOfBallsToFire = numberOfExtraBalls;
	    }
	}
}

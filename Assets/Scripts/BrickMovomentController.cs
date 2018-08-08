using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickMovomentController : MonoBehaviour {

    public enum brickState
    {
        stop,
        move
    }

    public brickState currentState;
    private bool hasMoved;

	// Use this for initialization
	void Start ()
	{
	    hasMoved = false;
	    currentState = brickState.stop;
	}

    void OnEnable()
    {
        hasMoved = false;
        currentState = brickState.stop;
    }
	
	// Update is called once per frame
	void Update () {
	    if (currentState == brickState.stop)
	    {
	        hasMoved = false;
	    }
	    if (currentState == brickState.move)
	    {
	        if (hasMoved == false)
	        {
                
	            transform.position = new Vector2(transform.position.x, transform.position.y - 1);
	            currentState = brickState.stop;
	            hasMoved = true;

	        }
	    }
	}
}

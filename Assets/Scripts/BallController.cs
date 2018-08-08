using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

public class BallController : MonoBehaviour
{

    public enum ballState
    {
        aim,
        fire,
        wait,
        endShot,
        endGame
    }

    public ballState currentBallState;

    public Rigidbody2D ball;
    public GameObject arrow;
    public GameObject reflectArrow;
    private GameManager gameManager;

    private Vector2 mouseStartPosition;
    private Vector2 mouseEndPosition;
    private Vector3 velosity;
    public Vector2 tempVelocity;
    public Vector2 launchVelocity;

    public Vector3 ballLounchPosition;

    public bool didClick;
    public bool didDrag;
    public bool canInteract;

    private float ballVelocityX;
    private float ballVelocityY;
    private float startY = 8;

    public float constantSpeed;

	// Use this for initialization
	void Start () {
        float leftSideOfScreen = Camera.main.transform.position.x - Camera.main.orthographicSize * Screen.width / Screen.height;
        //Debug.Log(leftSideOfScreen);
	    gameManager = FindObjectOfType<GameManager>();

	    currentBallState = ballState.aim;
        startY = arrow.GetComponent<LineRenderer>().GetPosition(1).y;
        gameManager.ballsInScene.Add(this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	    switch (currentBallState)
	    {
            case ballState.aim:
                if (Input.GetMouseButtonDown(0))
	            {
	                MouseClicked();
	            }
	            if (Input.GetMouseButton(0))
	            {
	                MouseDragged();
	            }
	            if (Input.GetMouseButtonUp(0))
	            {
	                ReleaseMouse();
	            }
                break;
            case ballState.fire:
	            break;
            case ballState.wait:
	            //currentBallState = ballState.endShot;
                Debug.Log("wait shot: " + gameManager.bricksInScene.Count);
                if (gameManager.ballsInScene.Count == 1)
                {
                    currentBallState = ballState.endShot;
                }
                break;
            case ballState.endShot:
                Debug.Log("End shot: " + gameManager.bricksInScene.Count);
	            for (int i = 0; i < gameManager.bricksInScene.Count; i++)
	            {
	                gameManager.bricksInScene[i].GetComponent<BrickMovomentController>().currentState = BrickMovomentController.brickState.move;
	            }
                gameManager.PlaceBricks();
                Debug.Log("Befour shot: ballState.aim");
	            currentBallState = ballState.aim;
                break;
            case ballState.endGame:

	            break;
            default:
                break;
	    }

	    
	}

    public void MouseClicked()
    {
        
        mouseStartPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Debug.Log(mouseStartPosition);
        
    }

    public void MouseDragged()
    {
        //OriginalDrag();
        MainDrag();

    }

    public void ReleaseMouse()
    {
        //OriginalReleaseMouse();
        MainReleaseMouse();
    }

    public void OriginalReleaseMouse()
    {
        arrow.SetActive(false);
        mouseEndPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        ballVelocityX = (mouseStartPosition.x - mouseEndPosition.x);
        ballVelocityY = (mouseStartPosition.y - mouseEndPosition.y);
        tempVelocity = new Vector2(ballVelocityX, ballVelocityY).normalized;
        ball.velocity = constantSpeed * tempVelocity;
        //
        launchVelocity = ball.velocity;
        //
        if (ball.velocity == Vector2.zero)
        {
            return;
        }
        ballLounchPosition = transform.position;
        currentBallState = ballState.fire;
    }

    public void MainReleaseMouse()
    {
        arrow.SetActive(false);
        reflectArrow.SetActive(false);
        mouseEndPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //ballVelocityX = (mouseStartPosition.x - mouseEndPosition.x);
        //ballVelocityY = (mouseStartPosition.y - mouseEndPosition.y);
        //Vector2 tempVelocity = new Vector2(ballVelocityX, ballVelocityY).normalized;
        //ball.velocity = tempVelocity*constantSpeed;
        ballVelocityX = velosity.x;
        ballVelocityY = velosity.y;
        tempVelocity = new Vector2(ballVelocityX, ballVelocityY).normalized;
        ball.velocity = tempVelocity * constantSpeed;
        launchVelocity = ball.velocity;
        //Debug.Log(ball.velocity + " " + tempVelocity);
        if (ball.velocity == Vector2.zero)
        {
            return;
        }
        ballLounchPosition = transform.position;
        currentBallState = ballState.fire;
    }

    private void MainDrag()
    {
        //Move the Arrow
        arrow.SetActive(true);
        Vector2 tempMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float diffX = mouseStartPosition.x / 500 - tempMousePosition.x;
        float diffY = 100 * mouseStartPosition.y / 500 - tempMousePosition.y;

        if (diffY <= 0)
        {
            diffY = 0.1f;
        }
        float diffTemp = diffX / diffY;
        //Mathf.Clamp(diffTemp, -4f, 4f);
        //Debug.Log("tx: " + diffTemp);
        float theta = Mathf.Rad2Deg * Mathf.Atan(diffTemp);
        Mathf.Clamp(theta, -85f, 85f);
        float ct = Mathf.Tan(theta / Mathf.Rad2Deg);



        float tX = startY * Mathf.Sin(ct);
        float tY = startY * Mathf.Cos(ct);
        if (tY < 1)
            return;
        velosity = new Vector3(tX, tY, 0);
        arrow.GetComponent<LineRenderer>().SetPosition(1, new Vector3(tX, tY, 0));

        Vector3 startPosition = transform.position;
        Vector3 endPosition = transform.TransformPoint(velosity);
        Ray ray = new Ray(startPosition, velosity);

        RaycastHit hit;
        Vector3 originalVector = velosity - startPosition;
        if (Physics.Raycast(ray, out hit, (velosity - startPosition).magnitude))
        {

            Debug.Log("Hit point: " + hit.point);
            if (hit.collider.tag == "Walls" && hit.point.y <= 3.5)
            {

                Vector3 tmp = arrow.GetComponent<LineRenderer>().transform.InverseTransformPoint(hit.point);
                Debug.Log("Hit wall: " + hit.point + " Local point : " + tmp);
                arrow.GetComponent<LineRenderer>().SetPosition(1, tmp);
                reflectArrow.transform.position = hit.point;

                Vector3 reflectedObject = Vector3.Reflect(hit.point - startPosition, hit.normal);
                LineRenderer _renderer = reflectArrow.GetComponent<LineRenderer>();
                _renderer.SetPosition(1, reflectedObject);
                reflectArrow.SetActive(true);

            }
            else
            {
                reflectArrow.SetActive(false);
            }
        }
        else
        {
            //line.SetPosition(1, ray.GetPoint(100));
            reflectArrow.SetActive(false);
        }
    }

    private void OriginalDrag()
    {
        arrow.SetActive(true);
        Vector2 tempMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float diffX = mouseStartPosition.x - tempMousePosition.x;
        float diffY = mouseStartPosition.y - tempMousePosition.y;
        if (diffY <= 0)
        {
            diffY = .01f;
        }
        float theta = Mathf.Rad2Deg * Mathf.Atan(diffX / diffY);
        arrow.transform.rotation = Quaternion.Euler(0f, 0f, -theta);
    }

}

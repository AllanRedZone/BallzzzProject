using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrickHealthManager : MonoBehaviour
{

    public int brickHealth;
    public Text brickHealthText;
    private GameManager gameManager;
    private ScoreManager score;
    public GameObject brickDestroyParticle;
    private SoundManager sound;

	// Use this for initialization
	void Start ()
	{
        brickHealthText = GetComponentInChildren<Text>();
        score = FindObjectOfType<ScoreManager>();
	    gameManager = FindObjectOfType<GameManager>();
	    brickHealth = gameManager.level;
	    sound = FindObjectOfType<SoundManager>();

	}

    void OnEnable()
    {
        gameManager = FindObjectOfType<GameManager>();
        brickHealth = gameManager.level;
    }

	// Update is called once per frame
	void Update ()
	{
	    brickHealthText.text = "" + brickHealth;
	    if (brickHealth <= 0)
	    {
	        //Destroy Brick
            score.IncreaseScore();
	        Instantiate(brickDestroyParticle, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
	    }
	}

    void TakeDamage(int damageToTake)
    {
        sound.ballHit.Play();
        brickHealth -= damageToTake;
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ball" || other.gameObject.tag == "ExtraBall")
        {
            TakeDamage(1);
        }
    }
}

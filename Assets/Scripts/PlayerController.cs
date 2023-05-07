using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float speed = 100.0f;
    private Rigidbody playerRb;
    public bool isGameOver=false;

    public Color[] colors = { Color.red, Color.green, Color.blue };

    HashSet<string> validBallTags = new HashSet<string>() { "RedBall", "BlueBall", "GreenBall" };

    GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        //make the player stand upright
        playerRb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        gameManager= GameObject.Find("GameManager").GetComponent<GameManager>();

        int randomIndex = Random.Range(0, colors.Length);
        Color randomColor = colors[randomIndex];
        GetComponent<Renderer>().material.color = randomColor;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MovePlayer();
        
    }
    void MovePlayer()
    {
        if (!isGameOver&&gameManager.score<10)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical);

            playerRb.AddForce(movement * speed);
        }
    }
   
    private void OnCollisionEnter(Collision collision)
    {
        //if (collision.gameObject.CompareTag("Ball"))
        if (validBallTags.Contains(collision.gameObject.tag))
        {
            if (GetComponent<Renderer>().material.color == collision.gameObject.GetComponent<Renderer>().material.color)
            {
                int randomIndex = Random.Range(0, colors.Length);
                Color randomColor = colors[randomIndex];
                GetComponent<Renderer>().material.color = randomColor;
                Destroy(collision.gameObject);
                gameManager.SpawnBall1second(collision.gameObject.tag);
                gameManager.UpdateScore(1);
            }
            else
            {
                isGameOver = true;
                gameManager.GameOver();

            }
        }
    }
}

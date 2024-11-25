using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBounce : MonoBehaviour
{
    public BallMovement ballmovement;
    public ScoreManager scoremanager;

    private void Bounce(Collision2D collision)
    {
        Vector3 ballPosition = transform.position;
        Vector3 racketPosition = collision.transform.position;
        float racketHeight = collision.collider.bounds.size.y;

        float postionX;
        if (collision.gameObject.name == "Player 1")
        {
            postionX = 1;

        }
        else
        {
            postionX = -1;
        }
        float postionY = (ballPosition.y -  racketPosition.y) / racketHeight;

        ballmovement.IncreaseHitCounter();
        ballmovement.moveBall(new Vector2(postionX, postionY));
       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Player 1" || collision.gameObject.name == "Player 2")
        {
            Bounce(collision);
        }
        else if(collision.gameObject.name =="Right Border")
        {
            scoremanager.Player1Goal();
            ballmovement.player1Start = false;
            StartCoroutine(ballmovement.Launch());
        }
        else if (collision.gameObject.name == "Left Border")
        {
            scoremanager.Player2Goal();
            ballmovement.player1Start = true;
            StartCoroutine(ballmovement.Launch());
        }

    }

}

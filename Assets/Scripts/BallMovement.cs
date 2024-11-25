using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    [SerializeField] public float StartSpeed;
    [SerializeField] public float ExtraSpeed;
    public float maxSpeed;

    public bool  player1Start = true;
    
    private int hitCounter;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb= GetComponent<Rigidbody2D>();

        StartCoroutine(Launch());
    }
    private void Restartball()
    { 
        rb.velocity = new Vector2(0, 0);
        transform.position = new Vector2(0, 0);
    }

    public IEnumerator Launch()
    {
        Restartball();
        hitCounter=0;
        yield return new WaitForSeconds(0.5f);

        if (player1Start == true) 
        {
            moveBall(new Vector2(-1, 0));
        }

        else
        {
            moveBall(new Vector2(1, 0));
        }
    }

    public void moveBall(Vector2 deriction)
    {
        deriction = deriction.normalized;

        float ballspeed= StartSpeed + hitCounter*maxSpeed;
        rb.velocity = deriction * ballspeed; 
    }

    public void IncreaseHitCounter()
    {
        if(hitCounter*ExtraSpeed < maxSpeed)
        {
            hitCounter++;
        }
    }

}    

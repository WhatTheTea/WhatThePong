
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    public delegate void BallScoredHandler(object sender, ScoreManager.Walls wall);
    public static event BallScoredHandler BallScored;

    private Rigidbody2D ball;
    public void Start()
    {
        ball = GetComponent<Rigidbody2D>();
        ThrowRandom();
    }
    public void ThrowRandom()
    {
        ball.position = Vector2.zero;
        float force = Random.Range(0, 2) == 0 ?
                      10 : -10;
        ball.velocity = Vector2.zero;
        ball.velocity = new Vector2(force, 0);
    }
    public void FixedUpdate()
    {
        // Кривой спидкэп
        if (ball.velocity.magnitude > 20) ball.velocity = ball.velocity.normalized * 20f;
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.collider.tag)
        {
            case "Player":
                //Отсокочить в направлении обратном центра игрока
                var force = new Vector2(0, -(collision.rigidbody.position.y - ball.position.y));
                ball.AddForce(force * 25f * collision.collider.attachedRigidbody.velocity.magnitude);
                //Если попал в центр, бахнуть рандомной силы
                if(collision.rigidbody.position.y > -1 && collision.rigidbody.position.y < 1)
                {
                    ball.AddForce(new Vector2(0, Random.Range(-10, 10)));
                }
                break;
            case "Finish":
                ScoreManager.Walls side = ball.position.x < 0 ? ScoreManager.Walls.Right : ScoreManager.Walls.Left;
                if(ball.position != Vector2.zero) BallScored?.Invoke(this, side);
                ThrowRandom();
                break;
            case "Wall":
                ball.AddForce(new Vector2(ball.velocity.x * 5f, 0));
                break;
        }
    }
}

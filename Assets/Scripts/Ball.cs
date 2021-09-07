
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public delegate void BallScoredHandler(object sender, ScoreManager.Walls wall);
    public static event BallScoredHandler BallScored;

    private Rigidbody2D _body;
    public Rigidbody2D Body => _body;

    public void Start()
    {
        _body = GetComponent<Rigidbody2D>();
        ThrowRandom();
    }
    private void ThrowRandom()
    {
        float direction = Random.Range(0, 2) == 0 
                      ? 10 
                      : -10; // Случайно определить куда кинуть мячик
        Body.position = Vector2.zero;
        Body.velocity = Vector2.zero;
        Body.velocity = new Vector2(direction, 0);
    }
    public void BounceBack(Collision2D collision)
    {
        var velocity = new Vector2(-collision.rigidbody.position.x * 2,
                    -(collision.rigidbody.position.y - Body.position.y) * 25);
        Body.velocity = velocity;
    }
    public void FixedUpdate()
    {
        // Кривой спидкэп
        if (Body.velocity.magnitude > 20) Body.velocity = Body.velocity.normalized * 20f;
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.collider.tag)
        {
            case "Player":
                //Отскочить в направлении обратном центра игрока
                BounceBack(collision);
                //Если попал в центр, бахнуть рандомной силы
                if(collision.rigidbody.position.y > -1 && collision.rigidbody.position.y < 1)
                {
                    Body.AddForce(new Vector2(0, Random.Range(0, 2) == 0
                      ? 20
                      : -20));
                }
                break;
            case "Finish":
                ScoreManager.Walls side = Body.position.x < 0 ? ScoreManager.Walls.Right : ScoreManager.Walls.Left;
                if(Body.position != Vector2.zero) BallScored?.Invoke(this, side);
                ThrowRandom();
                break;
            case "Wall":
                Body.velocity.Scale(new Vector2(2, 20));
                break;
        }
    }
}

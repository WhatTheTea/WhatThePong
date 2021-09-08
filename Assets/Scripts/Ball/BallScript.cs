
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public partial class BallScript
{
    //public delegate void BallScoredHandler(object sender, Walls wall);
    //public static event BallScoredHandler BallScored;

    private Rigidbody2D _body;
    public Rigidbody2D Body => _body;

    public void Start()
    {
        //_body = GetComponent<Rigidbody2D>();
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
    public void BounceBack(Collision2D from)
    {
        var velocity = new Vector2(-from.rigidbody.position.x * 2,
                    -(from.rigidbody.position.y - Body.position.y) * 10);
        Body.velocity = velocity;
    }
    public void FixedUpdate()
    {
        // Кривой спидкэп
        if (Body.velocity.magnitude > 17) Body.velocity = Body.velocity.normalized * 17f;
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        /*switch (collision.collider.tag)
        {
            case "Player":
                //Отскочить в направлении обратном центра игрока
                BounceBack(collision);
                if (collision.rigidbody.position.y - Body.position.y > -0.5f && collision.rigidbody.position.y - Body.position.y < 0.5f)
                {
                    Body.AddForce(new Vector2(0, Random.Range(0, 2) == 0
                      ? 10
                      : -10));
                }
                break;
            case "Finish":
                Walls side = Body.position.x < 0 ? Walls.Right : Walls.Left;
                if(Body.position != Vector2.zero) BallScored?.Invoke(this, side);
                ThrowRandom();
                break;
            case "Wall":
                Body.velocity.Scale(new Vector2(2, 1));
                break;
        }*/
    }
}

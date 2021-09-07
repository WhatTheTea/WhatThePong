using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _player;
    public Rigidbody2D Player { get => _player; }
    [SerializeField]
    private float _speed;
    public float Speed { get => _speed; }
    private string collidingWall = null;

    private void Start()
    {
        _player = GetComponent<Rigidbody2D>();
    }
    public void Move(Vector2 direction)
    {
        switch (collidingWall)
        {
            case "Up":
                //direction = new Vector2(0, -1);
                if (direction.y > 0) return;
                break;
            case "Down":
                if (direction.y < 0) return;
                break;
        }
        float offset = _speed * Time.deltaTime;
        _player.transform.Translate(direction * offset);
    }
    void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.collider.tag == "Wall")
        {
            collidingWall = collision.collider.name;
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        collidingWall = null; 
    }
}

using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Player : MonoBehaviour
{
    public static event System.EventHandler PlayerShot;

    private Rigidbody2D _body;
    public Rigidbody2D Body { get => _body; }
    [SerializeField]
    private float _speed;
    public float Speed { get => _speed; }
    private string collidingWall = null;

    private void Start()
    {
        _body = GetComponent<Rigidbody2D>();
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
        _body.transform.Translate(direction * offset);
    }
    public void Shoot()
    {
        PlayerShot.Invoke(this, System.EventArgs.Empty);
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

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
    private bool collidesWithWalls;
    public bool CollidesWithWalls { get => collidesWithWalls; }

    private void Start()
    {
        _player = GetComponent<Rigidbody2D>();
    }
    public void Move(Vector2 direction)
    {
        if (!collidesWithWalls)
        {
            var offset = _speed * Time.deltaTime;

            _player.transform.Translate(direction * offset);
        }
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
#if DEBUG
        if (collision.collider.attachedRigidbody.CompareTag("Wall"))
        {
            collidesWithWalls = true;
        }
#else
        collidesWithWalls = collision.collider.attachedRigidbody == "Wall";
#endif
    }
    public void OnCollisionExit2D(Collision2D collision)
    {
        collidesWithWalls = collision.collider.attachedRigidbody.tag != "Wall" && collidesWithWalls;
    }
}

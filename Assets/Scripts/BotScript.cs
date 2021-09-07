using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotScript : MonoBehaviour
{
    private Rigidbody2D _bot;
    private PlayerMovementScript _botMovement;
    [SerializeField]
    private Rigidbody2D _ball;
    void Start()
    {
        _bot = GetComponent<Rigidbody2D>();
        _botMovement = GetComponent<PlayerMovementScript>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _botMovement.Move(new Vector2(0, _ball.transform.position.y - _bot.transform.position.y));
    }
}

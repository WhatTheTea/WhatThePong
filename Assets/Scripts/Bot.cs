using System.Collections;
using System.Collections.Generic;
using System.Timers;

using UnityEngine;

public class Bot : MonoBehaviour //Прикрепляется к боту
{
    private Rigidbody2D _body;
    private PlayerMovement _botMovement;
    [SerializeField]
    private Rigidbody2D _ball;
    public Rigidbody2D Body => _body;
    public PlayerMovement BotMovement => _botMovement;
    public Rigidbody2D Ball => _ball;

    void Start()
    {
        _body = GetComponent<Rigidbody2D>();
        _botMovement = _body.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        BotMovement.Move(new Vector2(0, Ball.transform.position.y - Body.transform.position.y));
    }
}

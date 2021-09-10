using System.Collections;
using System.Collections.Generic;
using System.Timers;

using UnityEngine;

public class Bot : MonoBehaviour //Прикрепляется к боту
{
    private Rigidbody2D _body;
    private Player _botMovement;
    [SerializeField]
    private Rigidbody2D _ball;
    public Rigidbody2D Body => _body;
    public Player BotMovement => _botMovement;
    public Rigidbody2D Ball => _ball;

    void Start()
    {
        _body = GetComponent<Rigidbody2D>();
        _botMovement = _body.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        BotMovement.Move(new Vector2(0, (Ball.transform.position.y - Body.transform.position.y)));
    }
}

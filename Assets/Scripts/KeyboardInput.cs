using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInput : MonoBehaviour
{
    [SerializeField]
    private PlayerMovement _player1_movement;
    [SerializeField]
    private PlayerMovement _player2_movement;

    void Update()
    {
        var p1Axis = Input.GetAxis("Player1_Vertical");
        var p2Axis = Input.GetAxis("Player2_Vertical");

        if(p1Axis != 0) _player1_movement.Move(new Vector2(0, p1Axis));
        if(p2Axis != 0) _player2_movement.Move(new Vector2(0, p2Axis));
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInput : MonoBehaviour
{
    [SerializeField]
#pragma warning disable CS0649 
    private Player _player1;
    [SerializeField]
    private Player _player2;
#pragma warning restore CS0649

    void Update()
    {
        var p1Axis = Input.GetAxis("Player1_Vertical");
        var p2Axis = Input.GetAxis("Player2_Vertical");
        var p1Shoot = Input.GetButton("Fire1");
        var p2Shoot = Input.GetButton("Fire2");

        if (p1Axis != 0) _player1.Move(new Vector2(0, p1Axis));
        if (p2Axis != 0) _player2.Move(new Vector2(0, p2Axis));
        if (p1Shoot)
        {
            _player1.Shoot();
        }

        if (p2Shoot)
        {
            _player2.Shoot();
        }
    }
}

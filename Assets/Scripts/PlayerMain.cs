using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMain : MonoBehaviour
{
    private float _playerSpeed = 10f, _playerJump = 10f;
    private bool _isFacingRight = true;
    private Vector2 _moveXY = new Vector2(0f, 0f);
    public Rigidbody2D PlayerRig;


    void Update()
    {
        Move();
    }

    void Move()
    {
        _moveXY[0] = Input.GetAxis("Horizontal") * _playerSpeed;
        _moveXY[1] = Input.GetAxis("Vertical") * _playerSpeed;

        if( _moveXY[0] !=0 || _moveXY[1] !=0)
            PlayerRig.velocity = _moveXY;
    }

}

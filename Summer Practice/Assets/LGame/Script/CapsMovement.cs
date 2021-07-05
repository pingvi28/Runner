//CapsMovement
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsMovement : MonoBehaviour
{
    public float speedMove = 1.0f; //скорость перса
    public float jumpPower = 8.0f; // сила прыжка
    public int distance = 5;   // отвечает за то, в какой кубик мы перейдем 

    private float gravityForce, dirZ; // гравитаци€ перса
    private bool NeedToGo = true;
    private Vector3 moveVector; // направление движени€

    public CharacterController pl_controller;

    private void Start()
    {
        pl_controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        PlayerMove();
        GamingGravity();
    }

    public void Move()   // метод с параметром vectorMove, который отвечает за направлени€ в ту сторону, которую нужно двигатьс€
    {
        if (!NeedToGo)   // если мы не перемещаемс€ в данный момент
        {
            NeedToGo = true;   // двигаемс€ дальше
        }
    }

    private void PlayerMove()
    {
        Move();

        if (NeedToGo)
        {
            if (Input.GetMouseButtonDown(0))   // двигаемс€ влево
            {
                dirZ = speedMove;
            }
            else if (Input.GetMouseButtonDown(1))   // двигаемс€ вправо
            {
                dirZ = - speedMove;
            }

            NeedToGo = false;
        }

        moveVector.z = dirZ;
        moveVector.y = gravityForce; //расчет гравитации
        pl_controller.Move(moveVector * Time.deltaTime);
    }



    // чтобы падал
    private void GamingGravity()
    {
        if (!pl_controller.isGrounded) gravityForce -= 20f * Time.deltaTime;
        else gravityForce = -1f;

        // прыжок через прокрутку
        if (Input.mouseScrollDelta.y > 0.0f && pl_controller.isGrounded)
        {
            gravityForce = jumpPower;
        }
    }
}

/*
 // передвижение игрока
    private void PlayerMove()
    {
        //—читываем левый клик мыши, когда кнопка была отжата
        if (Input.GetMouseButtonUp(0) == true)
        {
            NeedToGo = true;
        }
        //если значение выставленно, то наш объект должен двигатьс€ в нужную сторону
        if (NeedToGo)
        {
            transform.position = Vector3.MoveTowards(transform.position, Input.mousePosition, speedMove * Time.deltaTime);
            NeedToGo = false;//¬ыключаем, если дошли
        }
   

        moveVector.y = gravityForce; //расчет гравитации
        pl_controller.Move(moveVector * Time.deltaTime);
    }*/

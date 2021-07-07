using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsMovement : MonoBehaviour
{
    public float speedMove = 5.0f; //скорость перса
    public float jumpPower = 8.0f; // сила прыжка
    public float distance = 0.1f;   // отвечает за то, в какой кубик мы перейдем 

    private float gravityForce, dirZ; // гравитация перса
    private bool NeedToGo = true;
    private Vector3 moveVector; // направление движения

    public CharacterController pl_controller;
    public Rigidbody rb;

    private void Start()
    {
        pl_controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        //PlayerMove();
        GamingGravity();
    }

    private void FixedUpdate() {
        PlayerMove();

        if (rb.position.y < -1f)
        {
            FindObjectOfType<GameManager>().EndGame();
        }
    }

    public void Move()   // метод: нужно ли двигаться
    {
        if (!NeedToGo)   // если мы не перемещаемся в данный момент
        {
            NeedToGo = true;   
        }
    }

    private void PlayerMove()
    {
        Move();

        if (NeedToGo)
        {
            if (Input.GetMouseButtonDown(0))   // двигаемся влево
            {
                dirZ = - speedMove;
            }
            else if (Input.GetMouseButtonDown(1))   // двигаемся вправо
            {
                dirZ =  speedMove;
            }

            NeedToGo = false;
        }

        moveVector.z = distance;
        moveVector.x = dirZ * 0.5f;
        moveVector.y = gravityForce; //расчет гравитации (прыжок)
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
        //Считываем левый клик мыши, когда кнопка была отжата
        if (Input.GetMouseButtonUp(0) == true)
        {
            NeedToGo = true;
        }
        //если значение выставленно, то наш объект должен двигаться в нужную сторону
        if (NeedToGo)
        {
            transform.position = Vector3.MoveTowards(transform.position, Input.mousePosition, speedMove * Time.deltaTime);
            NeedToGo = false;//Выключаем, если дошли
        }
   

        moveVector.y = gravityForce; //расчет гравитации
        pl_controller.Move(moveVector * Time.deltaTime);
    }*/

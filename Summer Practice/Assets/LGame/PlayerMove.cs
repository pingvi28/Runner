using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove2 : MonoBehaviour
{
    public float speedMove = 3.0f; //скорость перса
    public float jumpPower = 8.0f; // сила прыжка

    private float gravityForce; // гравитация перса
    private Vector3 moveVector; // направление движения

    public CharacterController pl_controller;
    private Animator pl_animator;
    private MobileController mContr;

    private void Start()
    {
        pl_controller = GetComponent<CharacterController>();
        pl_animator = GetComponent<Animator>();
        mContr = GameObject.FindGameObjectWithTag("Joystick").GetComponent<MobileController>();
    }

    private void Update()
    {
        PlayerMove();
        GamingGravity();
    }

    // передвижение игрока
    private void PlayerMove()
    {
        // перемещение по поверхности
        if (pl_controller.isGrounded)
        {
            pl_animator.ResetTrigger("Jump");
            pl_animator.SetBool("Falling", false);

            moveVector = Vector3.zero;
            moveVector.x = mContr.Horizontal() * speedMove;
            moveVector.z = mContr.Vertical() * speedMove;

            // анимация персонажа
            if (moveVector.x != 0 || moveVector.z != 0) pl_animator.SetBool("Move", true);
            else pl_animator.SetBool("Move", false);

            //поворот перса в пространстве
            if (Vector3.Angle(Vector3.forward, moveVector) > 1f || Vector3.Angle(Vector3.forward, moveVector) == 0)
            {
                Vector3 direct = Vector3.RotateTowards(transform.forward, moveVector, speedMove, 0.0f);
                transform.rotation = Quaternion.LookRotation(direct);
            }
        }
        else
        {
            if (gravityForce < -3f) pl_animator.SetBool("Falling", true);
        }


        moveVector.y = gravityForce; //расчет гравитации
        pl_controller.Move(moveVector * Time.deltaTime);
    }

    // чтобы падал
    private void GamingGravity()
    {
        if (!pl_controller.isGrounded) gravityForce -= 20f * Time.deltaTime;
        else gravityForce = -1f;

        // прыжок через пробел 
        /*
        if (Input.GetKeyDown(KeyCode.Space) && pl_controller.isGrounded)
        {
            gravityForce = jumpPower;
            pl_animator.SetTrigger("Jump");
        }
        */

        // прыжок через прокрутку
        if (Input.mouseScrollDelta.y > 0.0f && pl_controller.isGrounded)
        {
            gravityForce = jumpPower;
            pl_animator.SetTrigger("Jump");
        }
    }

    public void JumpButton()
    {
        gravityForce = jumpPower;
        pl_animator.SetTrigger("Jump");
    }
}


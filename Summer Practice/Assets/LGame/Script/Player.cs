using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private string statysMove;   // переменная отвечает за перемещение: перемещаемся ли в данный момент или нет?
    private string lastVectorMove;   // последний вектор перемещения
    public int distance;   // отвечает за то, в какой кубик мы перейдем 

    public void Move(string vectorMove)   // метод с параметром vectorMove, который отвечает за направления в ту сторону, которую нужно двигаться
    {
        if (statysMove != "move")   // если мы не перемещаемся в данный момент
        {
            lastVectorMove = vectorMove;
            statysMove = "move";   // двигаемся дальше
        }
    }

    private void FixedUpdate()
    {
        if (statysMove == "move")
        {
            if (lastVectorMove == "left")   // двигаемся влево
            {
                transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(transform.localPosition.x - distance, transform.localPosition.y, transform.localPosition.z), 0.1f);
            }
            else if (lastVectorMove == "right")   // двигаемся вправо
            {
                transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(transform.localPosition.x + distance, transform.localPosition.y, transform.localPosition.z), 0.1f);
            }

            statysMove = "";
        }
    }
}
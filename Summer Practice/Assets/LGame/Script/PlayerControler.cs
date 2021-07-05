using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    private string statysMove;   // переменная отвечает за перемещение: перемещаемся ли в данный момент или нет?
    private string lastVectorMove;   // последний вектор перемещения

    public void Move(string vectorMove)   // метод с параметром vectorMove, который отвечает за направления в ту сторону, которую нужно двигаться
    {
        if (statysMove != "move")   // если мы не перемещаемся в данный момент
        {
            lastVectorMove = vectorMove;
            statysMove = "move";   // двигаемся дальше
        }
    }
}
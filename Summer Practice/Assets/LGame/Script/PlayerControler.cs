using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    private string statysMove;   // ���������� �������� �� �����������: ������������ �� � ������ ������ ��� ���?
    private string lastVectorMove;   // ��������� ������ �����������

    public void Move(string vectorMove)   // ����� � ���������� vectorMove, ������� �������� �� ����������� � �� �������, ������� ����� ���������
    {
        if (statysMove != "move")   // ���� �� �� ������������ � ������ ������
        {
            lastVectorMove = vectorMove;
            statysMove = "move";   // ��������� ������
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private string statysMove;   // ���������� �������� �� �����������: ������������ �� � ������ ������ ��� ���?
    private string lastVectorMove;   // ��������� ������ �����������
    public int distance;   // �������� �� ��, � ����� ����� �� �������� 

    public void Move(string vectorMove)   // ����� � ���������� vectorMove, ������� �������� �� ����������� � �� �������, ������� ����� ���������
    {
        if (statysMove != "move")   // ���� �� �� ������������ � ������ ������
        {
            lastVectorMove = vectorMove;
            statysMove = "move";   // ��������� ������
        }
    }

    private void FixedUpdate()
    {
        if (statysMove == "move")
        {
            if (lastVectorMove == "left")   // ��������� �����
            {
                transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(transform.localPosition.x - distance, transform.localPosition.y, transform.localPosition.z), 0.1f);
            }
            else if (lastVectorMove == "right")   // ��������� ������
            {
                transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(transform.localPosition.x + distance, transform.localPosition.y, transform.localPosition.z), 0.1f);
            }

            statysMove = "";
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints1 : MonoBehaviour
{
    public GameObject[] waypoints;   // ������ � �������� �������, �� ������� ����� ��������� ���������
    int currentTarget = 0;   // ����� ������� ���� � ���������, �.�. ���� �� ������ ���������
    float rotSpeed;   // ��������
    public float speed;   //�������� ��������, ������� ����� ��������������
    float waypointRadius = 1;   // ����� ������ ��� ����, ����� ����������, ����� ��� ����� �� �����, � ��������� �������

    void Update()
    {
        if (Vector3.Distance(waypoints[currentTarget].transform.position, transform.position) < waypointRadius)  // ���� ����������, ������� ��������� ����� ������, � ������� ������ ������
        {                                                                                                        // � ����� ������ ��� ������, ����� ������ ��������� � ���������
            currentTarget++;
            if (currentTarget >= waypoints.Length)   // ���� ����� �� ����� ��������� �����, �� ������ ��������� � ����� ������ (0)
            {
                currentTarget = 0;
            }
        }

        // ����� ���������� ��� � ����������� �� ����, ��� �� ���������, ���� �� ������ ���������, � ��� ������ ��� ����� ���������
        transform.position = Vector3.MoveTowards(transform.position, waypoints[currentTarget].transform.position, speed * Time.deltaTime);

    }
}

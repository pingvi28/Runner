using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadGenerator : MonoBehaviour
{
    private List<GameObject> ReadyRoad = new List<GameObject>();   // ����������� ����� ���

    [Header("��� ������� ������")]
    public GameObject[] Road;

    public bool[] roadNumbers;   // ������ ��� ����, ����� ����� ����� ������� ����� ��������, � ����� ������

    [Header("������� ����� ������")]
    public int currentRoadLength = 0;

    [Header("������������ ����� ������")]
    public int maximumRoadLength = 6;

    [Header("�������� ����������� ������")]
    public float speedRoad = 5;

    [Header("������� �, ��� ������� �������� ������")]
    public float maximumPositionX = -15;

    [Header("���� ��������")]
    public Vector3 waitingZona = new Vector3(-40, 0, 0);

    [Header("��������� ����� ��������")]
    public float distanceBetweenRoads;

    private int currentRoadNumber = -1;   // ������� ����� ������
    private int lastRoadNumber = -1;   // ��������� ����� ������

    [Header("������ ���������")]
    public string roadGenerationStatus = "Generation";

    private void FixedUpdate()
    {
        if (roadGenerationStatus == "Generation")
        {
            if (currentRoadLength != maximumRoadLength)   // ���� ������� ����� ������ �� ����� ������������ ����� ������
            {
                currentRoadNumber = Random.Range(0, Road.Length);   // �� �������� �������� ����� ������ �� 0 �� ����� �������

                if (currentRoadNumber != lastRoadNumber)   // ���� ������� ����� ������ �� ����� ���������� ������ ������
                {
                    if (currentRoadNumber < Road.Length / 2)   // ���� ������� ����� ������ ������ �������� ����� ������� 
                    {
                        if (roadNumbers[currentRoadNumber] != true)   // ���� ������� ������ �� ������
                        {
                            if (lastRoadNumber != currentRoadNumber + (Road.Length / 2))   // ���� ��������� ����� ������ �� ����� �������� ���� �������� ����� ������
                            {
                                RoadCreation();   // �� ������� ������
                            }
                            else if (lastRoadNumber == currentRoadNumber + (Road.Length / 2) && currentRoadLength == Road.Length - 1) // ���� ������� ��������� ������� ������, 
                            {                                                                                                         // � ���������� ����� �� �������
                                RoadCreation();   // �� ������� ������
                            }
                        }
                    }
                    else if (currentRoadNumber >= Road.Length / 2)   //���� ������� ����� ������ ������ ��� ����� �������� ����� �������
                    {
                        if (roadNumbers[currentRoadNumber] != true)   // ���� ������� ������ �� ������
                        {
                            if (lastRoadNumber != currentRoadNumber - (Road.Length / 2))   // ���� �������� ����� ������ �� ����� �������� ����� �������� ����� ������ 
                            {
                                RoadCreation();   // �� ������� ������
                            }
                            else if (lastRoadNumber == currentRoadNumber - (Road.Length / 2) && currentRoadLength == Road.Length - 1) // ���� ������� ��������� ������� ������, 
                            {                                                                                                         // � ���������� ����� �� �������
                                RoadCreation();   // �� ������� ������
                            }
                        }
                    }
                }
            }

            MovingRoad();   // �������� ����� ����������� ������

            if (ReadyRoad.Count != 0)   // ���� ����� ������� ������� ������ ������ 0
            {
                RemovingRoad();   // �� �������� ����� �������� ������
            }
        }
    }

    private void MovingRoad()   // ������� ����� ����������� ������
    {
        foreach (GameObject readyRoad in ReadyRoad)   // ������� ����, � ������� ���������� ������ ������� ������� ������ �� ���������
        {
            readyRoad.transform.localPosition -= new Vector3(speedRoad * Time.fixedDeltaTime, 0f, 0f);
        }
    }

    private void RemovingRoad()
    {
        if (ReadyRoad[0].transform.localPosition.x < maximumPositionX)   // ���� ������� �� X ������� ������ � ������� 0 ������ ��� ���������� ������������ ������� X
        {
            int i;   // ������� ���������� i
            i = ReadyRoad[0].GetComponent<Road>().number;   // ����������� �� �������� ������ � ������� ������ � ������� 0
            roadNumbers[i] = false;   // ����������� ������ � ������� i �������� false
            ReadyRoad[0].transform.localPosition = waitingZona;   // ������� ������ � ������� 0 ���������� � ���� ��������
            ReadyRoad.RemoveAt(0);   // ������� ������� ������� ������ � ������� 0
            currentRoadLength--;   // ��������� ������� ����� ������ �� 1
        }
    }

    /* 
       � ������ RoadCreation() ���������, ���� ����� ������� ������� ������ = 0, �� ������� ������ � ������� ������� ���������� � ������� ����������, � ����
       ����� ������� ������� ������ ������ 0, �� ������� ������ � ������� ������� ���������� � ������� ���������� ������� ������ + Vector3 � ���������� �� X  
    */

    private void RoadCreation()   // ����� �������� ������
    {
        if (ReadyRoad.Count > 0)
        {
            Road[currentRoadNumber].transform.localPosition = ReadyRoad[ReadyRoad.Count - 1].transform.position + new Vector3(distanceBetweenRoads, 0f, 0f);
        }
        else if (ReadyRoad.Count == 0)
        {
            Road[currentRoadNumber].transform.localPosition = new Vector3(0f, 0f, 0f);
        }

        Road[currentRoadNumber].GetComponent<Road>().number = currentRoadNumber;   // ������ � ������� ������� ����������� �������� ���������� number ������� ����� ������ 
        roadNumbers[currentRoadNumber] = true;   // ������ ������ ������
        lastRoadNumber = currentRoadNumber;   // ���������� ������ ����������� �������� �������� ������
        ReadyRoad.Add(Road[currentRoadNumber]);   // � ������ ������� ������ ��������� ������ � ������� ������� ������
        currentRoadLength++;   // ����������� ����� ������ �� ���� 
    }
}

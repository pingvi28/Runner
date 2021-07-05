using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadGenerator : MonoBehaviour
{
    private List<GameObject> ReadyRoad = new List<GameObject>();   // заполняется через код

    [Header("Все участки дороги")]
    public GameObject[] Road;

    public bool[] roadNumbers;   // массив для того, чтобы знать какие участки дорог свободны, а какие заняты

    [Header("Текущая длина дороги")]
    public int currentRoadLength = 0;

    [Header("Максимальная длина дороги")]
    public int maximumRoadLength = 6;

    [Header("Скорость перемещения дороги")]
    public float speedRoad = 5;

    [Header("Позиция Х, при которой исчезает дорога")]
    public float maximumPositionX = -15;

    [Header("Зона ожидания")]
    public Vector3 waitingZona = new Vector3(-40, 0, 0);

    [Header("Дистанция между дорогами")]
    public float distanceBetweenRoads;

    private int currentRoadNumber = -1;   // текущий номер дороги
    private int lastRoadNumber = -1;   // последний номер дороги

    [Header("Статус генерации")]
    public string roadGenerationStatus = "Generation";

    private void FixedUpdate()
    {
        if (roadGenerationStatus == "Generation")
        {
            if (currentRoadLength != maximumRoadLength)   // если текущая длина дороги не равна максимальной длине дороги
            {
                currentRoadNumber = Random.Range(0, Road.Length);   // то рандомно выбераем номер дороги от 0 до длины массива

                if (currentRoadNumber != lastRoadNumber)   // если текущий номер дороги не равен последнему номеру дороги
                {
                    if (currentRoadNumber < Road.Length / 2)   // если текущий номер дороги меньше половины длины массива 
                    {
                        if (roadNumbers[currentRoadNumber] != true)   // если текущая дорога не занята
                        {
                            if (lastRoadNumber != currentRoadNumber + (Road.Length / 2))   // если последний номер дороги не равен текущему плюс половина длины дороги
                            {
                                RoadCreation();   // то создаем дорогу
                            }
                            else if (lastRoadNumber == currentRoadNumber + (Road.Length / 2) && currentRoadLength == Road.Length - 1) // если остался последний участок дороги, 
                            {                                                                                                         // а предыдущий такой же участок
                                RoadCreation();   // то создаем дорогу
                            }
                        }
                    }
                    else if (currentRoadNumber >= Road.Length / 2)   //если текущий номер дороги больше или равен половины длины массива
                    {
                        if (roadNumbers[currentRoadNumber] != true)   // если текущая дорога не занята
                        {
                            if (lastRoadNumber != currentRoadNumber - (Road.Length / 2))   // если последий номер дороги не равен текущему минус половина длины дороги 
                            {
                                RoadCreation();   // то создаем дорогу
                            }
                            else if (lastRoadNumber == currentRoadNumber - (Road.Length / 2) && currentRoadLength == Road.Length - 1) // если остался последний участок дороги, 
                            {                                                                                                         // а предыдущий такой же участок
                                RoadCreation();   // то создаем дорогу
                            }
                        }
                    }
                }
            }

            MovingRoad();   // вызываем метод перемещения дороги

            if (ReadyRoad.Count != 0)   // если длина массива текущей дороги больше 0
            {
                RemovingRoad();   // то вызываем метод удаления дороги
            }
        }
    }

    private void MovingRoad()   // создаем метод перемещения дороги
    {
        foreach (GameObject readyRoad in ReadyRoad)   // создаем цикл, в котором перемещаем каждый участок готовой дороги со скоростью
        {
            readyRoad.transform.localPosition -= new Vector3(speedRoad * Time.fixedDeltaTime, 0f, 0f);
        }
    }

    private void RemovingRoad()
    {
        if (ReadyRoad[0].transform.localPosition.x < maximumPositionX)   // если позиция по X готовой дороги с номером 0 меньше чем переменная максимальная позиция X
        {
            int i;   // создаем переменную i
            i = ReadyRoad[0].GetComponent<Road>().number;   // присваиваем ей значение номера с готовой дороги с номером 0
            roadNumbers[i] = false;   // присваиваем дороге с номером i значение false
            ReadyRoad[0].transform.localPosition = waitingZona;   // готовую дорогу с номером 0 перемещаем в зону ожидания
            ReadyRoad.RemoveAt(0);   // удаляем участок готовой дороги с номером 0
            currentRoadLength--;   // уменьшаем текущую длину дороги на 1
        }
    }

    /* 
       в методе RoadCreation() проверяем, если длина массива готовой дороги = 0, то участок дороги с текущим номером перемещаем в нулевые координаты, а если
       длина массива готовой дороги больше 0, то участок дороги с текущим номером перемещаем в позицию предыдущей готовой дороги + Vector3 с дистанцией по X  
    */

    private void RoadCreation()   // метод создания дороги
    {
        if (ReadyRoad.Count > 0)
        {
            Road[currentRoadNumber].transform.localPosition = ReadyRoad[ReadyRoad.Count - 1].transform.position + new Vector3(distanceBetweenRoads, 0f, 0f);
        }
        else if (ReadyRoad.Count == 0)
        {
            Road[currentRoadNumber].transform.localPosition = new Vector3(0f, 0f, 0f);
        }

        Road[currentRoadNumber].GetComponent<Road>().number = currentRoadNumber;   // дороге с текущим номером присваиваем значение переменной number текущий номер дороги 
        roadNumbers[currentRoadNumber] = true;   // дорога теперь занята
        lastRoadNumber = currentRoadNumber;   // последнему номеру присваиваем значение текущего номера
        ReadyRoad.Add(Road[currentRoadNumber]);   // в массив готовой дороги добавляем дорогу с текущим номером дороги
        currentRoadLength++;   // увеличиваем длину дороги на один 
    }
}

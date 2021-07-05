using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints3 : MonoBehaviour
{
    public GameObject[] waypoints;   // массив с будущими точками, по которым будет двигаться платформа
    int currentTarget = 0;   // какая текущая цель у платформы, т.е. куда он должен двигаться
    float rotSpeed;   // скорость
    public float speed;   //скорость движения, которая будет регалироваться
    float waypointRadius = 1;   // точка служит для того, чтобы определить, когда куб дошел до конца, и вернуться обратно

    void Update()
    {
        if (Vector3.Distance(waypoints[currentTarget].transform.position, transform.position) < waypointRadius)  // если расстояние, которое находится между точкой, в которую должны прийти
        {                                                                                                        // и кубом меньше чем радиус, тогда должны двигаться к следующей
            currentTarget++;
            if (currentTarget >= waypoints.Length)   // если дошли до самой последней точки, то должны вернуться к самой первой (0)
            {
                currentTarget = 0;
            }
        }

        // далее перемещаем куб в зависимости от того, где он находится, куда он должен двигаться, и как быстро это долно произойти
        transform.position = Vector3.MoveTowards(transform.position, waypoints[currentTarget].transform.position, speed * Time.deltaTime);

    }
}

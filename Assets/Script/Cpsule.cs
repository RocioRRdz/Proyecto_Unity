using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Capsule : MonoBehaviour
{
    public Transform[] waypoints; // Array para almacenar los waypoints
    public float speed = 2f; // Velocidad de movimiento de la cápsula
    private int currentWaypointIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Lee el archivo de texto
        string filePath = "C:/AyR_2024_Unidad3/Unidad3/Assets/Script/camino.txt";
        string[] waypointNames = File.ReadAllLines(filePath);

        Debug.Log("Waypoints leídos del archivo:");

        // Asigna los waypoints basados en los nombres leídos del archivo de texto
        waypoints = new Transform[waypointNames.Length];
        for (int i = 0; i < waypointNames.Length; i++)
        {
            GameObject waypoint = GameObject.Find(waypointNames[i]);
            if (waypoint != null)
            {
                waypoints[i] = waypoint.transform;
                Debug.Log("Waypoint asignado: " + waypointNames[i]);
            }
            else
            {
                Debug.LogWarning("Waypoint not found: " + waypointNames[i]);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Si no se han alcanzado todos los waypoints
        if (currentWaypointIndex < waypoints.Length && waypoints[currentWaypointIndex] != null)
        {
            // Mueve la cápsula hacia el siguiente waypoint
            transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypointIndex].position, speed * Time.deltaTime);

            Debug.Log("Moviéndose hacia: " + waypoints[currentWaypointIndex].name);

            // Si la cápsula está lo suficientemente cerca del waypoint, pasa al siguiente
            if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex].position) < 1.05f)
            {
                Debug.Log("Llegó al waypoint: " + waypoints[currentWaypointIndex].name);
                currentWaypointIndex++;
            }
        }
    }

    void OnDrawGizmos()
    {
    if (waypoints != null)
    {
        for (int i = 0; i < waypoints.Length; i++)
        {
            if (waypoints[i] != null)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(waypoints[i].position, 0.2f);
                
                if (i < waypoints.Length - 1 && waypoints[i + 1] != null)
                {
                    Gizmos.color = Color.blue;
                    Gizmos.DrawLine(waypoints[i].position, waypoints[i + 1].position);
                }
            }
        }
    }
    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bear : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private GameObject player;
    private static Vector3 positionPlayer;
    private static Transform transformPlayer;

    [Header("Variables")]

    [SerializeField] Rigidbody2D bearRb;
    [SerializeField] private int speed = 30;
    [SerializeField] private int range = 10;

    [SerializeField] private int detectionDistance = 30;
    // Start is called before the first frame update
    void Start()
    {
        transformPlayer = player.GetComponent<Transform>();  
        bearRb = GetComponent<Rigidbody2D>();  
    }

    // Update is called once per frame
    void Update()
    {
        positionPlayer = transformPlayer.position;
        if (DistanceToPlayer() <= detectionDistance)
        {
            MoveTowardsPlayer();
        }
        

        

        
    }

    private float DistanceToPlayer()
    {
        // Calcular la distancia al jugador
        if (player != null)
        {
            return Vector3.Distance(transform.position, player.transform.position);
        }
        return Mathf.Infinity; // Retorna infinito si el jugador no está disponible
    }

    private void MoveTowardsPlayer()
    {
        if (player != null)
        {
            // Calculate the direction to the player
            Vector3 lookDirection = (player.transform.position - transform.position).normalized;
            

            // Calculate angle in radians and convert to degrees
            float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;

            // Create the rotation towards the player in 2D
            Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));

            // Rotate the spider smoothly towards the player's direction.
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * speed);

            // Move to player
            Vector3 movement = lookDirection * speed * Time.deltaTime;
            bearRb.MovePosition(transform.position + movement);
        }
    }

}

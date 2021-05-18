using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraComtroller : MonoBehaviour
{
    [SerializeField] private Transform player;
    private Vector3 pos;  // Сюда будем записывать координаты движения

    private void Awake()
    {
        if (!player)
            player = FindObjectOfType<Hero>().transform;
    }

    private void Update()
    {
        pos = player.position;
        pos.z = -10f;

        transform.position = Vector3.Lerp(transform.position, pos, Time.deltaTime);  // Метод Lerp делает движение более плавным
    }
}

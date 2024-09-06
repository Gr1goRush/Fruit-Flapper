using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public GameObject[] objectsToSpawn; // Массив объектов для спауна
    public Vector2 spawnArea; // Область спауна по оси X
    public float spawnInterval = 1f; // Интервал времени между спаунами
    private float spawnTimer = 0f;
    [SerializeField] GameObject coin, heart;
    [SerializeField] int RareHeartDrop;
    [SerializeField] int RareCoinDrop;
    private void Update()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnInterval)
        {
            SpawnObject();
            spawnTimer = 0f;
        }
    }

    private void SpawnObject()
    {
        if (Random.Range(0, RareCoinDrop) == RareCoinDrop / 2)
        {
            SpawnObjectCoin();
        }
        else if (Random.Range(0, RareHeartDrop) == RareHeartDrop / 2)
        {
            SpawnObjectHeart();
        }
        else
        {
            SpawnObjectRandom();
        }
        
    }
    private void SpawnObjectRandom()
    {
        
        float spawnX = Random.Range(spawnArea.x, spawnArea.y); // Генерируем случайное значение по оси X в заданной области
        Vector3 spawnPosition = new Vector3(spawnX, transform.position.y, transform.position.z); // Создаем позицию спауна
        GameObject objectToSpawn = objectsToSpawn[Random.Range(0, objectsToSpawn.Length)]; // Выбираем случайный объект из массива
        Instantiate(objectToSpawn, spawnPosition, Quaternion.identity); // Спауним выбранный объект
    }
    private void SpawnObjectCoin()
    {

        float spawnX = Random.Range(spawnArea.x, spawnArea.y); // Генерируем случайное значение по оси X в заданной области
        Vector3 spawnPosition = new Vector3(spawnX, transform.position.y, transform.position.z); // Создаем позицию спауна
        
        Instantiate(coin, spawnPosition, Quaternion.identity); // Спауним выбранный объект
    }
    private void SpawnObjectHeart()
    {

        float spawnX = Random.Range(spawnArea.x, spawnArea.y); // Генерируем случайное значение по оси X в заданной области
        Vector3 spawnPosition = new Vector3(spawnX, transform.position.y, transform.position.z); // Создаем позицию спауна
       
        Instantiate(heart, spawnPosition, Quaternion.identity); // Спауним выбранный объект
    }

}

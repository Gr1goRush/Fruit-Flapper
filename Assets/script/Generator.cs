using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public GameObject[] objectsToSpawn; // ������ �������� ��� ������
    public Vector2 spawnArea; // ������� ������ �� ��� X
    public float spawnInterval = 1f; // �������� ������� ����� ��������
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
        
        float spawnX = Random.Range(spawnArea.x, spawnArea.y); // ���������� ��������� �������� �� ��� X � �������� �������
        Vector3 spawnPosition = new Vector3(spawnX, transform.position.y, transform.position.z); // ������� ������� ������
        GameObject objectToSpawn = objectsToSpawn[Random.Range(0, objectsToSpawn.Length)]; // �������� ��������� ������ �� �������
        Instantiate(objectToSpawn, spawnPosition, Quaternion.identity); // ������� ��������� ������
    }
    private void SpawnObjectCoin()
    {

        float spawnX = Random.Range(spawnArea.x, spawnArea.y); // ���������� ��������� �������� �� ��� X � �������� �������
        Vector3 spawnPosition = new Vector3(spawnX, transform.position.y, transform.position.z); // ������� ������� ������
        
        Instantiate(coin, spawnPosition, Quaternion.identity); // ������� ��������� ������
    }
    private void SpawnObjectHeart()
    {

        float spawnX = Random.Range(spawnArea.x, spawnArea.y); // ���������� ��������� �������� �� ��� X � �������� �������
        Vector3 spawnPosition = new Vector3(spawnX, transform.position.y, transform.position.z); // ������� ������� ������
       
        Instantiate(heart, spawnPosition, Quaternion.identity); // ������� ��������� ������
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fruit : MonoBehaviour
{
    public float fallSpeed = 2f; // �������� ������� �������
    public float lifetime = 5f; // ����� ����� �������
    private float timer = 0f;

    private void Update()
    {
        // ������� ������� ������� ����
        transform.Translate(Vector3.down * (fallSpeed + PlayerPrefs.GetFloat("acceleration")) * Time.deltaTime);

        // ������ ��� �������� ������� ����� ���������� �������
        timer += Time.deltaTime;
        if (timer >= lifetime)
        {
            Destroy(gameObject);
        }
    }
}

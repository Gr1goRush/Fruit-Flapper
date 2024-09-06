using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fruit : MonoBehaviour
{
    public float fallSpeed = 2f; // Скорость падения объекта
    public float lifetime = 5f; // Время жизни объекта
    private float timer = 0f;

    private void Update()
    {
        // Плавное падение объекта вниз
        transform.Translate(Vector3.down * (fallSpeed + PlayerPrefs.GetFloat("acceleration")) * Time.deltaTime);

        // Таймер для удаления объекта после указанного времени
        timer += Time.deltaTime;
        if (timer >= lifetime)
        {
            Destroy(gameObject);
        }
    }
}

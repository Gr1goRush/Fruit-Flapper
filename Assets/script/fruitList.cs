using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class fruitList : MonoBehaviour
{
    public string playerTag = "Player"; // ��� ������
    public float springForce = 0f; // ���� �������

    private void Start()
    {
        // ����� ������ �� ����
        GameObject player = GameObject.FindGameObjectWithTag(playerTag);

        // ���� ����� ������
        if (player != null)
        {

            // ������� Spring Joint �� ���� ������� � ���������� ��� � ������
            float distance = Vector3.Distance(player.transform.position, gameObject.transform.position);
            SpringJoint2D springJoint = gameObject.AddComponent<SpringJoint2D>();
            springJoint.connectedBody = player.GetComponent<Rigidbody2D>();
            springJoint.autoConfigureDistance = false;
            springJoint.dampingRatio = 1;
            springJoint.distance = distance;
            springJoint.frequency = springForce; // ������� ������� (����)
           Rigidbody2D rb= gameObject.GetComponent<Rigidbody2D>();
            rb.gravityScale = 1f;
            rb.drag = 3f;
            rb.mass = 1000;
        }
    }
    }

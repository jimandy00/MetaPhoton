using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 5f;

    // character controller ���� ����
    CharacterController cc;

    // ���� �Ŀ�
    float jumpPower = 5f;

    // �߷�
    float gravity = -9.81f;

    // y �ӷ�
    float yVelocity = 0f;

    // Start is called before the first frame update
    void Start()
    {
        // character controller ��������
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // W, S, A, DŰ�� ������ �յ��¿�� �����̰� �ʹ�.
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 dir = new Vector3(h, 0, v);
     // Vector3 dirH = transform.right * h;
     // Vector3 dirV = transform.forward * v;
     // Vector3 dir -dirH + dirV;
        dir.Normalize(); // �ҹ���, �빮�� ����?

        // ���� ���� ��Ҵٸ�?
        if(cc.isGrounded)
        {
            // yVelocity�� 0���� ����
            yVelocity = 0;
        }
        
        // �����̽��ٸ� ������ ����
        if (Input.GetKeyDown(KeyCode.Space))
        {
            yVelocity = jumpPower;
        }

        // yVelocity�� �߷¸�ŭ ���ҽ�Ű��
        yVelocity += gravity * Time.deltaTime;

        // yVelocity���� dir�� y���� ����
        dir.y = yVelocity;

        // �� �������� ��������
        // transform.position += dir * speed * Time.deltaTime;
        cc.Move(dir * speed * Time.deltaTime);
    }
}

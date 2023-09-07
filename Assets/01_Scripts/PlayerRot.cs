using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerRot : MonoBehaviourPun
{
    float speed = 200f;

    // ȸ����
    float mx = 0f;
    float my = 0f;

    // ī�޶�
    public Transform cam;

    // Start is called before the first frame update
    void Start()
    {
        // ���� ������ player�� ī�޶� ��
        if (photonView.IsMine) // true�� ���� ����.
        {
            // cam.gameObject.SetActive(true);
            cam.GetChild(0).gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // ������ �ƴҶ� �Լ��� ������
        if (photonView.IsMine == false)
        {
            return;
        }

        // ���� ���콺 Ŀ���� Ȱ��ȭ �Ǿ������� �Լ��� ������
        if(Cursor.visible == true) { return; }
        
        // ���콺 �����ӵ��� �÷��̾ �¿� ȸ��
        // ī�޶� ���Ʒ� ȸ��

        // 1. ���콺 �Է� �ޱ�
        float x = Input.GetAxisRaw("Mouse X");
        float y = Input.GetAxisRaw("Mouse Y");

        // 2. ���콺�� ������ �� ����
        mx += x * speed * Time.deltaTime;
        my += y * speed * Time.deltaTime;

        my = Mathf.Clamp(my, -90f, 90f);

        // 3. ������ ����ŭ �ش� ������Ʈ�� ȸ��
        transform.localEulerAngles = new Vector3(0, mx, 0);
        cam.transform.localEulerAngles = new Vector3(-my, 0, 0);

    }
}

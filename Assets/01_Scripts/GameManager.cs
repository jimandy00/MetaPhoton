using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // ���� �÷��̾� ����
        // ���� ������ �����Ǳ淡 Y�� 1�� ����
        PhotonNetwork.Instantiate("Player", new Vector3(0, 1, 0), Quaternion.identity);
        

        // OnPhotonSerialization ȣ�� ��
        PhotonNetwork.SerializationRate = 60;

        // ���콺 ������ ��Ȱ��ȭ
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        // ���� ESC Ű�� ������
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            // ���콺 ������ Ȱ��ȭ
            Cursor.visible = true;
        }


        // ���콺�� Ŭ������ ��
        if (Input.GetMouseButtonDown(0))
        {
            // ���콺 Ŭ���ÿ� �ش� ��ġ�� UI�� ������
            // current == ���콺 Ŀ��
            // IsPointerOverGameObject == UI(2D)
            if(EventSystem.current.IsPointerOverGameObject() == false)
            {
                // ���콺 ������ ��Ȱ��ȭ
                Cursor.visible = false;
            }
            
        }
        
    }
}

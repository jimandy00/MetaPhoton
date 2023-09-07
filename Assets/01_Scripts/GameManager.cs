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
        // 나의 플레이어 생성
        // 땅에 박혀서 생성되길래 Y값 1로 변경
        PhotonNetwork.Instantiate("Player", new Vector3(0, 1, 0), Quaternion.identity);
        

        // OnPhotonSerialization 호출 빈도
        PhotonNetwork.SerializationRate = 60;

        // 마우스 포인터 비활성화
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        // 만약 ESC 키를 누르면
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            // 마우스 포인터 활성화
            Cursor.visible = true;
        }


        // 마우스를 클릭했을 때
        if (Input.GetMouseButtonDown(0))
        {
            // 마우스 클릭시에 해당 위치에 UI가 없으면
            // current == 마우스 커서
            // IsPointerOverGameObject == UI(2D)
            if(EventSystem.current.IsPointerOverGameObject() == false)
            {
                // 마우스 포인터 비활성화
                Cursor.visible = false;
            }
            
        }
        
    }
}

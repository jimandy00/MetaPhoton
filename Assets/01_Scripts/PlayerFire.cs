using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerFire : MonoBehaviourPun
{

    // bomb factory
    public GameObject bombFactory;

    // 파편 공장
    public GameObject fragmentFactory;

    string nickName;



    // Start is called before the first frame update
    void Start()
    {
        // 내가 만든 플레이어가 아닐 때
        // PlayerFire 컴포넌트를 비활성화한다.
        if (photonView.IsMine == false)
        {
            this.enabled = false;
            nickName = photonView.Owner.NickName; 
        }

    }

    // Update is called once per frame
    void Update()
    {
        // 1번 키를 누르면
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            // 만들어진 폭탄을 카메라가 보는 방향으로 1만큼 떨어진 지점에 놓는다.
            Vector3 pos = Camera.main.transform.position + Camera.main.transform.forward * 1;

            // 만들어진 총알의 앞방향을 카메라가 보는 방향으로 설정하자
            Vector3 forward = Camera.main.transform.forward;

            // 총알을 인스턴스로 만들어주는 함수
            photonView.RPC(nameof(FireBulletByRpc), RpcTarget.All, pos, forward);
        }

        // 2번 키를 누르면
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            photonView.RPC(nameof(FireRay), RpcTarget.All, Camera.main.transform.position, Camera.main.transform.forward);
            ;
        }

        // 만약에 Ray를 발사해서 부딪힌 곳이 있다면?
        // 구조체에 대해서 학습하기! vlaue, reference type? value type입니다.


    }
    void FireBulletByInstantiate()
    {

        // 만들어진 폭탄을 카메라가 보는 방향으로 1만큼 떨어진 지점에 놓는다.
        Vector3 pos = Camera.main.transform.position + Camera.main.transform.forward * 1;

        // 만들어진 총알의 앞방향을 카메라가 보는 방향으로 설정하자
        Quaternion rot = Camera.main.transform.rotation;

        // 폭탄 공장에서 폭탄을 만들고,
        GameObject bomb = PhotonNetwork.Instantiate("Bomb", pos, rot);
    }

    [PunRPC]
    void FireBulletByRpc(Vector3 firePos, Vector3 fireForward)
    {
        GameObject bomb = Instantiate(bombFactory);
        bomb.transform.position = firePos;
        bomb.transform.forward = fireForward;
    }

    [PunRPC]
    void FireRay(Vector3 cameraPosition, Vector3 cameraForward)
    {
        // 카메라 위치, 카메라 앞방향으로 Ray를 만들자
        Ray ray = new Ray(cameraPosition, cameraForward);

        RaycastHit hitInfo; // value type... reference type처럼 쓰기위해 out

        if (Physics.Raycast(ray, out hitInfo)) // out
        {
            // 그 위치에 파편효과 공장에서 파편효과를 만든다.
            GameObject fragment = Instantiate(fragmentFactory);

            // 만들어진 파편효과를 부딪힌 위치에 놓는다.
            fragment.transform.position = hitInfo.point;

            // 파편효과의 방향을 부딪힌 위치의 normal 방향으로 설정
            fragment.transform.forward = hitInfo.normal;

            // 2초 뒤에 파편효과를 제거
            Destroy(fragment, 2);

            // 만약 맞은 놈의 이름이 Player를 포함?
            if (hitInfo.transform.gameObject.name.Contains("Player"))
            {
                // 플레이어가 가진 PlayerHP 컴포넌트 가져오고
                PlayerHP hp = hitInfo.transform.GetComponent<PlayerHP>();
                // 가져온 컴포넌트의 UpateHP 함수 실행
                hp.UpdateHP(-10);
            }

        }
    }
}

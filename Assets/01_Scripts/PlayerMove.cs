using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using TMPro;

public class PlayerMove : MonoBehaviourPun, IPunObservable
{
    public float speed = 5f;

    // character controller 담을 변수
    CharacterController cc;

    // 점프 파워
    float jumpPower = 5f;

    // 중력
    float gravity = -9.81f;

    // y 속력
    float yVelocity = 0f;

    // 서버에서 넘어오는 위치값
    Vector3 receivePos;

    // 서버에서 넘어오는 회전값
    Quaternion receiveRot = Quaternion.identity;

    // 보관하는 속력
    float lerpSpeed = 50f;

    // 닉네임을 가져오자
    public Text nickName;



    // Start is called before the first frame update
    void Start()
    {
        // character controller 가져오자
        cc = GetComponent<CharacterController>();

        // 닉네임 설정
        nickName.text = photonView.Owner.NickName;



    }

    // Update is called once per frame
    void Update()
    {
        // 내가 만든 플레이어라면
        if (photonView.IsMine == true)
        {
            // W, S, A, D키를 누르면 앞뒤좌우로 움직이고 싶다.
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");

            Vector3 dir = new Vector3(h, 0, v);
            // Vector3 dirH = transform.right * h;
            // Vector3 dirV = transform.forward * v;
            // Vector3 dir -dirH + dirV;
            dir.Normalize(); // 소문자, 대문자 차이?

            // 만약 땅에 닿았다면?
            if (cc.isGrounded)
            {
                // yVelocity를 0으로 하자
                yVelocity = 0;
            }

            // 스페이스바를 누르면 점프
            if (Input.GetKeyDown(KeyCode.Space))
            {
                yVelocity = jumpPower;
            }

            // yVelocity를 중력만큼 감소시키기
            yVelocity += gravity * Time.deltaTime;

            // yVelocity값을 dir의 y값에 셋팅
            dir.y = yVelocity;

            // 그 방향으로 움직여라
            // transform.position += dir * speed * Time.deltaTime;
            cc.Move(dir * speed * Time.deltaTime);
        }
        // 나의 플레이어가 아니라면? == 내 pc에서 다른 사람에 해당됨
        else
        {
            // 위치, 회전 값을 보관(보정)
            transform.position = Vector3.Lerp(transform.position, receivePos, lerpSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, receiveRot, lerpSpeed * Time.deltaTime);
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        // 내 플레이어면(isMine과 같이 동작하는 걸로 구현할거임 : stream 내가 데이터를 받고/보낼 수 있는 상태인지. ㅐㅜ ㄴㄷ갸미 여기서 사용되는 것임)
        // 나의 위치, 회전값을 보낸다.
        if(stream.IsWriting)
        {
            stream.SendNext(transform.position); // 레퍼런스 값은 안되고 value값을 보내야함
            stream.SendNext(transform.rotation); // 오일러 어쩌고로도 보낼 수 있음
        }
        // 내 플레이어가 아니라면
        // 위치, 회전값을 받는다.
        else
        {
            // SendNext 작성한 순서대로 들어감. 약간.. n : n 느낌?
            receivePos = (Vector3)stream.ReceiveNext();
            receiveRot = (Quaternion)stream.ReceiveNext();
        }
    }
}

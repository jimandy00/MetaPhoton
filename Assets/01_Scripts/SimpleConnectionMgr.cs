using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class SimpleConnectionMgr : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        // 시작 하자마자 접속
        // photon 환경설정을 기반으로 접속을 시도한다.
        PhotonNetwork.ConnectUsingSettings();


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 마스터서버 접속 완료될 때 호출되는 함수
    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();

        print(nameof(OnConnectedToMaster));

        // 로비 진입
        JoinLobby();
    }

    // 로비 진입을 하는 함수
    void JoinLobby()
    {
        // 닉네임 설정
        PhotonNetwork.NickName = "동식이";

        // 기본 Lobby 입장
        PhotonNetwork.JoinLobby();
    }

    // 로비 진입 완료가 됐을 대 호출되는 함수
    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();

        print(nameof(OnJoinedLobby));

        // 방을 만들거나 참여
        RoomOptions roomOption = new RoomOptions() ;
        PhotonNetwork.JoinOrCreateRoom("meta_unity_room", roomOption, TypedLobby.Default);
    }

    // 방 생성 완료시 호출 되는 함수
    public override void OnCreatedRoom()
    {
        base.OnCreatedRoom();

        print(nameof(OnCreatedRoom));
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);

        print(nameof(OnCreateRoomFailed));

        // 방 생성 실패 원인을 보여주는 팝업(숙제)
    }

    // 방 참여 성공시 호출되는 함수
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();

        print(nameof(OnJoinedRoom));

        // GameScene으로 이동
        PhotonNetwork.LoadLevel("GameScene");
    }

}

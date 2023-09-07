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
        // ���� ���ڸ��� ����
        // photon ȯ�漳���� ������� ������ �õ��Ѵ�.
        PhotonNetwork.ConnectUsingSettings();


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // �����ͼ��� ���� �Ϸ�� �� ȣ��Ǵ� �Լ�
    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();

        print(nameof(OnConnectedToMaster));

        // �κ� ����
        JoinLobby();
    }

    // �κ� ������ �ϴ� �Լ�
    void JoinLobby()
    {
        // �г��� ����
        PhotonNetwork.NickName = "������";

        // �⺻ Lobby ����
        PhotonNetwork.JoinLobby();
    }

    // �κ� ���� �Ϸᰡ ���� �� ȣ��Ǵ� �Լ�
    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();

        print(nameof(OnJoinedLobby));

        // ���� ����ų� ����
        RoomOptions roomOption = new RoomOptions() ;
        PhotonNetwork.JoinOrCreateRoom("meta_unity_room", roomOption, TypedLobby.Default);
    }

    // �� ���� �Ϸ�� ȣ�� �Ǵ� �Լ�
    public override void OnCreatedRoom()
    {
        base.OnCreatedRoom();

        print(nameof(OnCreatedRoom));
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);

        print(nameof(OnCreateRoomFailed));

        // �� ���� ���� ������ �����ִ� �˾�(����)
    }

    // �� ���� ������ ȣ��Ǵ� �Լ�
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();

        print(nameof(OnJoinedRoom));

        // GameScene���� �̵�
        PhotonNetwork.LoadLevel("GameScene");
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using TMPro;

public class PlayerMove : MonoBehaviourPun, IPunObservable
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

    // �������� �Ѿ���� ��ġ��
    Vector3 receivePos;

    // �������� �Ѿ���� ȸ����
    Quaternion receiveRot = Quaternion.identity;

    // �����ϴ� �ӷ�
    float lerpSpeed = 50f;

    // �г����� ��������
    public Text nickName;



    // Start is called before the first frame update
    void Start()
    {
        // character controller ��������
        cc = GetComponent<CharacterController>();

        // �г��� ����
        nickName.text = photonView.Owner.NickName;



    }

    // Update is called once per frame
    void Update()
    {
        // ���� ���� �÷��̾���
        if (photonView.IsMine == true)
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
            if (cc.isGrounded)
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
        // ���� �÷��̾ �ƴ϶��? == �� pc���� �ٸ� ����� �ش��
        else
        {
            // ��ġ, ȸ�� ���� ����(����)
            transform.position = Vector3.Lerp(transform.position, receivePos, lerpSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, receiveRot, lerpSpeed * Time.deltaTime);
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        // �� �÷��̾��(isMine�� ���� �����ϴ� �ɷ� �����Ұ��� : stream ���� �����͸� �ް�/���� �� �ִ� ��������. ���� �������� ���⼭ ���Ǵ� ����)
        // ���� ��ġ, ȸ������ ������.
        if(stream.IsWriting)
        {
            stream.SendNext(transform.position); // ���۷��� ���� �ȵǰ� value���� ��������
            stream.SendNext(transform.rotation); // ���Ϸ� ��¼��ε� ���� �� ����
        }
        // �� �÷��̾ �ƴ϶��
        // ��ġ, ȸ������ �޴´�.
        else
        {
            // SendNext �ۼ��� ������� ��. �ణ.. n : n ����?
            receivePos = (Vector3)stream.ReceiveNext();
            receiveRot = (Quaternion)stream.ReceiveNext();
        }
    }
}

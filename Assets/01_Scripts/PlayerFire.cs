using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerFire : MonoBehaviourPun
{

    // bomb factory
    public GameObject bombFactory;

    // ���� ����
    public GameObject fragmentFactory;

    string nickName;



    // Start is called before the first frame update
    void Start()
    {
        // ���� ���� �÷��̾ �ƴ� ��
        // PlayerFire ������Ʈ�� ��Ȱ��ȭ�Ѵ�.
        if (photonView.IsMine == false)
        {
            this.enabled = false;
            nickName = photonView.Owner.NickName; 
        }

    }

    // Update is called once per frame
    void Update()
    {
        // 1�� Ű�� ������
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            // ������� ��ź�� ī�޶� ���� �������� 1��ŭ ������ ������ ���´�.
            Vector3 pos = Camera.main.transform.position + Camera.main.transform.forward * 1;

            // ������� �Ѿ��� �չ����� ī�޶� ���� �������� ��������
            Vector3 forward = Camera.main.transform.forward;

            // �Ѿ��� �ν��Ͻ��� ������ִ� �Լ�
            photonView.RPC(nameof(FireBulletByRpc), RpcTarget.All, pos, forward);
        }

        // 2�� Ű�� ������
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            photonView.RPC(nameof(FireRay), RpcTarget.All, Camera.main.transform.position, Camera.main.transform.forward);
            ;
        }

        // ���࿡ Ray�� �߻��ؼ� �ε��� ���� �ִٸ�?
        // ����ü�� ���ؼ� �н��ϱ�! vlaue, reference type? value type�Դϴ�.


    }
    void FireBulletByInstantiate()
    {

        // ������� ��ź�� ī�޶� ���� �������� 1��ŭ ������ ������ ���´�.
        Vector3 pos = Camera.main.transform.position + Camera.main.transform.forward * 1;

        // ������� �Ѿ��� �չ����� ī�޶� ���� �������� ��������
        Quaternion rot = Camera.main.transform.rotation;

        // ��ź ���忡�� ��ź�� �����,
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
        // ī�޶� ��ġ, ī�޶� �չ������� Ray�� ������
        Ray ray = new Ray(cameraPosition, cameraForward);

        RaycastHit hitInfo; // value type... reference typeó�� �������� out

        if (Physics.Raycast(ray, out hitInfo)) // out
        {
            // �� ��ġ�� ����ȿ�� ���忡�� ����ȿ���� �����.
            GameObject fragment = Instantiate(fragmentFactory);

            // ������� ����ȿ���� �ε��� ��ġ�� ���´�.
            fragment.transform.position = hitInfo.point;

            // ����ȿ���� ������ �ε��� ��ġ�� normal �������� ����
            fragment.transform.forward = hitInfo.normal;

            // 2�� �ڿ� ����ȿ���� ����
            Destroy(fragment, 2);

            // ���� ���� ���� �̸��� Player�� ����?
            if (hitInfo.transform.gameObject.name.Contains("Player"))
            {
                // �÷��̾ ���� PlayerHP ������Ʈ ��������
                PlayerHP hp = hitInfo.transform.GetComponent<PlayerHP>();
                // ������ ������Ʈ�� UpateHP �Լ� ����
                hp.UpdateHP(-10);
            }

        }
    }
}

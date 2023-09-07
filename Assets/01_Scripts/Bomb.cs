using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Bomb : MonoBehaviourPun
{
    // �Ѿ� �ӷ�
    float speed = 10f;

    // ����ȿ�� ����
    public GameObject explosionFactory;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // PhotonNetwork.Instantiate �̿��� ��
        // ���� �� �Ѿ˸� �����̰� ����
/*        if (photonView.IsMine)
        {
            // ��� ������ ����ʹ�.
            transform.position += speed * transform.forward * Time.deltaTime;
        }*/
        // ��� ������ ����ʹ�.
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    // �浹���� ��
    private void OnTriggerEnter(Collider other)
    {
        // ���� ȿ��
        GameObject explosion = Instantiate(explosionFactory);
        explosion.transform.position = transform.position;
        ParticleSystem particle = explosion.GetComponent<ParticleSystem>();
        particle.Play();
        // 2�ʵڿ� particle system �ı�
        Destroy(explosion, 2);

/*        // ���� �� �Ѿ˸� �ı�����
        if(photonView.IsMine)
        {
            // ���� �ı��ϱ�
            PhotonNetwork.Destroy(gameObject);

        }*/



    }


}

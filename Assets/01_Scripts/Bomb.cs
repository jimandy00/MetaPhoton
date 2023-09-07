using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Bomb : MonoBehaviourPun
{
    // 총알 속력
    float speed = 10f;

    // 폭발효과 공장
    public GameObject explosionFactory;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // PhotonNetwork.Instantiate 이용할 때
        // 내가 쏜 총알만 움직이게 하자
/*        if (photonView.IsMine)
        {
            // 계속 앞으로 가고싶다.
            transform.position += speed * transform.forward * Time.deltaTime;
        }*/
        // 계속 앞으로 가고싶다.
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    // 충돌했을 때
    private void OnTriggerEnter(Collider other)
    {
        // 폭발 효과
        GameObject explosion = Instantiate(explosionFactory);
        explosion.transform.position = transform.position;
        ParticleSystem particle = explosion.GetComponent<ParticleSystem>();
        particle.Play();
        // 2초뒤에 particle system 파괴
        Destroy(explosion, 2);

/*        // 내가 쏜 총알만 파괴하자
        if(photonView.IsMine)
        {
            // 나를 파괴하기
            PhotonNetwork.Destroy(gameObject);

        }*/



    }


}

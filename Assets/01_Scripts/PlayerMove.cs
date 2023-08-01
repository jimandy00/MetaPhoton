using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
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

    // Start is called before the first frame update
    void Start()
    {
        // character controller 가져오자
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
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
        if(cc.isGrounded)
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
}

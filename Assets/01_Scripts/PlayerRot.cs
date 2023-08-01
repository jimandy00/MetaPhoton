using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRot : MonoBehaviour
{
    float speed = 200f;

    // 회전값
    float mx = 0f;
    float my = 0f;

    // 카메라
    public Transform cam;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // 마우스 움직임따라 플레이어를 좌우 회전
        // 카메라를 위아래 회전

        // 1. 마우스 입력 받기
        float x = Input.GetAxisRaw("Mouse X");
        float y = Input.GetAxisRaw("Mouse Y");

        // 2. 마우스의 움직임 값 누적
        mx += x * speed * Time.deltaTime;
        my += y * speed * Time.deltaTime;

        my = Mathf.Clamp(my, -90f, 90f);

        // 3. 누적된 값만큼 해당 오브젝트를 회전
        transform.localEulerAngles = new Vector3(0, mx, 0);
        cam.transform.localEulerAngles = new Vector3(-my, 0, 0);

    }
}
